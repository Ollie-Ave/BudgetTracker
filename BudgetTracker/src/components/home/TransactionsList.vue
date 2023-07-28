<script setup>
import { ref, onMounted } from 'vue';

import TransactionItem from '@/components/home/TransactionItem.vue';
import Modal from '@/components/shared/Modal.vue';

import { useApiRoutes } from '@/stores/apiRoutes';
import { useAccountData } from '@/stores/accountData';
import { useLoginStore } from '@/stores/loggedIn';

import handleApiError from '@/sharedLogic/handleApiErrors';

import { toast } from 'vue3-toastify'
import axios from 'axios';
import SpinnerButton from '@/components/shared/SpinnerButton.vue';

const apiRoutesStore = useApiRoutes();
const accountDataStore = useAccountData();
const loggedInStore = useLoginStore();

const accountData = await accountDataStore.getAccountData(loggedInStore.apiKey);

const emit = defineEmits(['balanceUpdated']);

const transactions = ref([]);
const currentTransactionsPage = ref(0);
const moreToShow = ref(true);

const editSaveButton = ref(null);
const loadMoreButton = ref(null);

const showCreateNewModal = ref(false);
const newTransactionModel = ref({
    placeOfPurchase: '',
    amount: 0,
    timeOfPurchase: ''
});

onMounted(async () => {
    await loadMoreTransactions();
})

async function loadMoreTransactions() {
    try {
        const response = await axios.get(`${apiRoutesStore.transactionsUrl}/${accountData.uid}?apiKey=${loggedInStore.apiKey}&page=${currentTransactionsPage.value}`);

        response.data.forEach(transaction => {
            transactions.value.push(transaction);
        });
        currentTransactionsPage.value += response.data.length;

        if (response.data.length < 10) {
            moreToShow.value = false;
        }
        else {
            moreToShow.value = true;
            loadMoreButton?.value?.resetState();
        }
    }
    catch (error) {
        await handleApiError(error);
    }
}

async function deleteTransaction(id) {
    try {
        const newBalance = await axios.delete(`${apiRoutesStore.transactionsUrl}/${id}?apiKey=${loggedInStore.apiKey}`);

        accountDataStore.updateBalance(newBalance.data);
        
        await removeTransactionFromList(id, newBalance.data);

        emit('balanceUpdated');
        
        toast.success('Transaction Deleted');
    }
    catch (error) {
        await handleApiError(error);
    }
}

async function removeTransactionFromList(id, newBalance) {
    document.getElementById(id).classList.add('deleted');

    setTimeout(async () => {
        const index = transactions.value.findIndex(transaction => transaction.uid === id);

        transactions.value.splice(index, 1);

        const elementsWithDeletedClass = document.getElementsByClassName('deleted');

        for (const element of elementsWithDeletedClass) {
            element.classList.remove('deleted');
        }
        
        await refreshTransactions(newBalance);
    }, 600);    
}

async function refreshTransactions(newBalance) {
    accountDataStore.updateBalance(newBalance);
    transactions.value = [];

    const pageIncrementValue = 10;
    const oldPageValue = currentTransactionsPage.value - pageIncrementValue;

    currentTransactionsPage.value = 0;
    if (oldPageValue < 0) {
        console.log(oldPageValue)
        await loadMoreTransactions();
    }
    else {
        for (currentTransactionsPage.value; currentTransactionsPage.value <= oldPageValue;) {
            await loadMoreTransactions();
        }
    }

    emit('balanceUpdated');
}

function toggleAddTransactionModal() {
    showCreateNewModal.value = !showCreateNewModal.value;
}

async function addTransaction() {
    const validationResult = validateInputtedTransaction(newTransactionModel.value);

    if (validationResult === true) {
        try {
            const newBalance = await axios.post(`${apiRoutesStore.transactionsUrl}/${accountData.uid}?apiKey=${loggedInStore.apiKey}`, newTransactionModel.value);
            
            await refreshTransactions(newBalance.data);
            
            editSaveButton.value.resetState();
    
            newTransactionModel.value = {
                placeOfPurchase: '',
                amount: 0,
                timeOfPurchase: ''
            };
    
            toggleAddTransactionModal();

            toast.success('Transaction Added');
        }
        catch (error) {
            await handleApiError(error);
        }
    }
    else {
        toast.error(validationResult);

        editSaveButton.value.resetState();
    }
}

function validateInputtedTransaction(transaction) {
    let returnValue = true;

    if (transaction.placeOfPurchase === '' ||
        transaction.amount === 0 ||
        transaction.timeOfPurchase === '') {
        returnValue = 'Please fill out all fields';
    }
    else if (new Date(transaction.timeOfPurchase) > new Date()){
        returnValue = 'New Transaction Dates Cannot Be In The Future.';
    }

    return returnValue;
}
</script>

<template>
    <div class="container">
        <div class="title-container">
            <div class="title">
                <i class="bi bi-calendar-range"></i>
                <h3> Your Transaction History</h3>
            </div>
            <hr>
        </div>

        <div class="transactions">
            <div v-for="transaction in transactions">
                <div class="transaction"  :id="transaction.uid">
                    <TransactionItem :transaction=transaction @deleteTransaction="deleteTransaction(transaction.uid)" @refreshTransactions="refreshTransactions"/>
                </div>
                <hr>
            </div>

            <div class="load-more" v-if="moreToShow">
                <SpinnerButton ref="loadMoreButton" :clickCallback="loadMoreTransactions" buttonText='Load More' />
            </div>
            <div class="load-more" v-if="moreToShow == false">
                <p class="text-muted">That's it! You've no more transactions to show.</p>
            </div>
        </div>

        <div class="footer-container">
            
            <hr>

            <div class="footer">
                <div class="title">
                    <i class="bi bi-wallet"></i>
                    <h4>Missing Transaction?</h4>
                </div>
    
                <button class="btn btn-proceed" @click=toggleAddTransactionModal>Add New.</button>
            </div>

            <Modal  :showModal=showCreateNewModal transition="fade">
                <form>
                    <div class="new-transaction">
                        <div class="new-title">
                            <h2>Add New Transaction</h2>
                            <hr>
                        </div>
                        
                        <div class="input-field">
                            <label for="placeOfPurchase">Place of Purchase:</label>
                            <input type="text" v-model="newTransactionModel.placeOfPurchase" class="text-input"/>
                        </div>
                        <div class="input-field">
                            <label for="amount">Amount:</label>
                            <input type="number" step=".01" v-model="newTransactionModel.amount" class="text-input" />
                        </div>

                        <div class="input-field">
                            <label for="timeOfPurchase">Time of Purchase:</label>
                            <input type="datetime-local" v-model="newTransactionModel.timeOfPurchase" class="text-input date-input" />
                        </div>

                        <div class="buttons">
                            <button class="btn btn-warning" @click.prevent="toggleAddTransactionModal">Cancel</button>

                            <SpinnerButton ref="editSaveButton" buttonText='Save' :clickCallback="addTransaction" />
                        </div>
                    </div>
                </form>
            </Modal>
        </div>
    </div>
</template>

<style scoped>
.container{
    position: relative;
    height: 80vh;
    overflow-y: scroll;

    display: flex;
    flex-direction: column;
    
    -ms-overflow-style: none;  /* IE and Edge */
    scrollbar-width: 0px;  /* Firefox */
}

::-webkit-scrollbar {
  display: none;
}

.load-more{
    margin: 1rem auto;

    display: flex;
    justify-content: center;
}

.deleted{
    transform: translate(100%, 0px);
    transition: 0.5s ease-in-out;
}

hr {
    margin: 0px;
}

.title {
    padding: 0.75rem;

    display: flex;
    justify-content: center;
    align-items: center;
}

.title-container{
    position: sticky;
    top: 0px;
    background-color: var(--colour-surface-200);
}

.title > i {
    font-size: 2rem;
}

.title > h3, h4 {
    margin-left: 1rem;
}

.load-more > .btn{
    margin: 2rem auto;
    width: 7rem;
}

.footer-container{
    position: sticky;
    bottom: -1px;
    background-color: var(--colour-surface-200);
    width: 100%;

    margin-top: auto;
}

.footer{
    display: flex;
    justify-content: space-between;
    align-items: center;

    margin: 0rem 0.75rem;
}


.new-transaction{

display: flex;
flex-direction: column;
align-items: center;

width: 40vw;
}

.new-title > h2{
margin-bottom: 0.5rem;
}

.new-title{
width: 100%;
margin-bottom: 1.5rem;
}

.input-field{
margin-bottom: 1rem;

display: flex;
flex-direction: column;

align-items: center;

width: 80%;
}

.text-input{
background-color: var(--colour-surface-100-transparent);
margin-bottom: 1rem;
}

.input-field > label{
margin-bottom: 1rem;
}

::-webkit-calendar-picker-indicator {
filter: invert(1);
}

input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
-webkit-appearance: none;
margin: 0;
}


.buttons{
    display: flex;
    justify-content: space-around;
    align-items: center;

    width: 100%;
}
.center{
    text-align: center;
    margin-bottom: 2rem;
}

.buttons > * {
    padding: 0.5rem 1.25rem;
}

/* Firefox */
input[type=number] {
-moz-appearance: textfield;
appearance: textfield;
}
</style>