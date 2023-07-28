<script setup>
import { ref, computed } from 'vue';

import formatCurrency from '@/sharedLogic/formatCurrency';
import Modal from '../shared/Modal.vue';

import handleApiError from '@/sharedLogic/handleApiErrors';

import axios from 'axios';

import { useLoginStore } from '@/stores/loggedIn';
import { useApiRoutes } from '@/stores/apiRoutes';
import SpinnerButton from '@/components/shared/SpinnerButton.vue';

const apiRoutesStore = useApiRoutes();
const loggedInStore = useLoginStore();

const props = defineProps(['transaction']);

const updatedTransaction = ref({
    uid: props.transaction.uid,
    placeOfPurchase: props.transaction.placeOfPurchase,
    amount: props.transaction.amount.toFixed(2),
    timeOfPurchase: props.transaction.timeOfPurchase,
    balanceAfterPurchase: props.transaction.balanceAfterPurchase,
    balanceBeforePurchase: props.transaction.balanceBeforePurchase
});

const emit = defineEmits(['deleteTransaction', 'refreshTransactions']);

const showMenu = ref(false);
const showAreYouSureModal = ref(false);
const showEditModal = ref(false);
const modalTransition = ref('fade');

const deleteTransactionButton = ref(null);
const ammountIsPositive = ref(props.transaction.amount > 0 ? 'positive' : 'negative');

const formattedTimeOfTransaction = computed({
    // Everybody loves JS dates... right?
    get() {
        return new Date(updatedTransaction.value.timeOfPurchase).toISOString().slice(0, 16);
    },
    set(newValue) {
        updatedTransaction.value.timeOfPurchase = newValue;
    }
})

function toggleMenu() {
    function eventListener() {
        toggleMenu();

        document.removeEventListener('mousedown', eventListener);
    } 
    
    showMenu.value = !showMenu.value;

    if (showMenu.value){
        document.addEventListener('mousedown', eventListener);
    }
}

function toggleAreYouSureModal() {
    showAreYouSureModal.value = !showAreYouSureModal.value;
}

function toggleEditModal() {
    showEditModal.value = !showEditModal.value;
}

function deleteTransaction() {
    modalTransition.value = 'none';
    toggleAreYouSureModal();
    deleteTransactionButton.value.resetState();

    emit('deleteTransaction', props.transaction.uid)
}

async function saveTransaction() {
    try {
        const newBalance = await axios.put(`${apiRoutesStore.transactionsUrl}/${props.transaction.uid}?apiKey=${loggedInStore.apiKey}`, updatedTransaction.value);
        emit('refreshTransactions', newBalance.data);
    }
    catch (error) {
        handleApiError();
    }

    toggleEditModal();
}

</script>

<template>
    <div class="container">
        <div class="left">
            <img src="@/assets/images/icons/dollar.png" alt="Transaction Image" />
            
            <div class="text">
                <h4>{{ transaction.placeOfPurchase }}</h4>
                <p class="text-muted">{{ new Date(transaction.timeOfPurchase).toLocaleDateString('en-gb', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' }) }}</p>
            </div>
        </div>

        <div class="right">
            <p  :class=ammountIsPositive>{{ formatCurrency(transaction.amount) }}</p>
            <i class="bi bi-three-dots-vertical" @click="toggleMenu"></i>
            <div class="sub-menu">
                <transition name="slide" mode="out-in">
                    <div class="menu" v-show="showMenu">
                        <div class="menu-item" @click="toggleEditModal">
                            <i class="bi bi-pencil"></i>
                            <p>edit</p>
                        </div>
                        <div class="menu-item delete-container delete" @click="toggleAreYouSureModal">
                            <i class="bi bi-trash delete"></i>
                            <p class="delete">delete</p>
                        </div>
                    </div>
                </transition>

            </div>
        </div>
    </div>

    <modal :showModal=showEditModal :transition=modalTransition>
        <form>
            <div class="edit-transaction">
                <div class="edit-title">
                    <h2>Edit Transaction</h2>
                    <hr>
                </div>
                
                <div class="input-field">
                    <label for="placeOfPurchase">Place of Purchase:</label>
                    <input type="text" v-model="updatedTransaction.placeOfPurchase" class="text-input"/>
                </div>
                <div class="input-field">
                    <label for="amount">Amount:</label>
                    <input type="number" step=".01" v-model="updatedTransaction.amount" class="text-input" />
                </div>

                <div class="input-field">
                    <label for="timeOfPurchase">Time of Purchase:</label>
                    <input type="datetime-local" v-model="formattedTimeOfTransaction" class="text-input date-input" />
                </div>

                <div class="buttons">
                    <button class="btn btn-warning" @click.prevent="toggleEditModal">Cancel</button>
                    <button class="btn btn-proceed" @click.prevent="saveTransaction">Save</button>
                </div>
            </div>
        </form>
    </modal>
    
    <modal :showModal=showAreYouSureModal :transition=modalTransition>
        <h3>Are you sure you want to delete this transaction?</h3>
        <p class="text-muted center">You cannot undo this action.</p>
        <div class="buttons">
            <button class="btn btn-warning" @click="toggleAreYouSureModal">No</button>

            <SpinnerButton ref="deleteTransactionButton" :clickCallback="deleteTransaction" buttonText="Yes" />
        </div>
    </modal>
</template>

<style scoped>
.slide-enter-active {
  animation: slide-in 0.2s;
}
.slide-leave-active {
  animation: slide-in 0.2s reverse;
}
@keyframes slide-in {
  0% {
    height: 0px;
  }
  100% {
    height: 5rem;
  }
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

.buttons > button{
    padding: 0.5rem 1.25rem;
}

.sub-menu{
    position: relative;
}

.menu{
    overflow-y: hidden;
    position: absolute;
    top: 41px;
    right: -11px;

    background-color: var(--colour-surface-300);

    z-index: 10;
}

.menu-item{
    padding: 0.5rem 2rem;
    display: flex;

    transition: 0.2s ease;
    cursor: pointer;
}

.delete-container:hover{
    color: var(--colour-primary-100);
}

.menu-item:hover{
    background-color: var(--colour-surface-400);
}

.menu-item > p {
    margin: 0px;
    margin-left: 1rem;
}

.container{
    display: flex;
    justify-content: space-between;
    align-items: center;

    padding: 0.75rem;

    z-index: -1;
}

.left{
    display: flex;
    align-items: center;

    gap: 1rem;
}

.text > h4, .text > p{
    margin: 0.2rem;
}

img {
    width: 1.5rem;
    aspect-ratio: 1/1;

    padding: 0.75rem;

    border-style: solid;
    border-width: 1px;

    background-color: #0000000a;

    border-radius: 0.75rem;
}

.right{
    display: flex;
    align-items: center;
    gap: 1rem;
}

.right > i{
    font-size: 1.25rem;

    transition: 0.2s ease;
}

.right > i:hover{
    cursor: pointer;
    color: var(--colour-primary-100);
}
.edit-transaction{

    display: flex;
    flex-direction: column;
    align-items: center;

    width: 40vw;
}

.edit-title > h2{
    margin-bottom: 0.5rem;
}

.edit-title{
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

/* Firefox */
input[type=number] {
  -moz-appearance: textfield;
  appearance: textfield;
}
</style>