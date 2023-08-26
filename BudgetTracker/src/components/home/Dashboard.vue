<script setup>
import { ref, watch, onMounted } from 'vue';

import DashboardCard from '@/components/home/DashboardCard.vue';
import TransactionList from '@/components/home/TransactionsList.vue';

import { useAccountData } from '@/stores/accountData';
import { useLoginStore } from '@/stores/loggedIn';
import { useApiRoutes } from '@/stores/apiRoutes';

import handleApiError from '@/sharedLogic/handleApiErrors';

import axios from 'axios';
import SpendingChart from './SpendingChart.vue';

const accountDataStore = useAccountData();
const loggedInStore = useLoginStore();
const apiRoutesStore = useApiRoutes();

let accountData = null;

// Account balance feature
const balance = ref(null);

async function updateAccountBalance()
{
    balance.value = await accountDataStore.getAccountData(loggedInStore.apiKey);
}

// Expenses Data feature
const expenses = ref(null);

async function getAccountExpenses(uid) {
    try {
        const queryString = `apiKey=${loggedInStore.apiKey}&days=30&totalType=expenses`;

        const response = await axios.get(`${apiRoutesStore.dayTotalUrl}/sum/${uid}?${queryString}`);
        expenses.value = response.data;
    }
    catch (error) {
        await handleApiError(error);

        return 0;
    }
}

watch(balance, async () => {
    if (accountData !== null)
    {
        await getAccountExpenses(accountData.uid);
    }
});

// Account Income feature

const income = ref(null);

async function getAccountIncome(uid) {
    try {
        const response = await axios.get(`${apiRoutesStore.totalIncomeUrl}/${uid}?apiKey=${loggedInStore.apiKey}`);
        return response.data;
    }
    catch (error) {
        await handleApiError(error);

        return 0;
    }
}

// On Mount

onMounted(async () => {
     accountData = await accountDataStore.getAccountData(loggedInStore.apiKey);

    balance.value = accountData.balance;

    income.value = await getAccountIncome(accountData.uid)
});


</script>

<template>

    <div class="dashboard">
        <div class="left">
            <div class="top">
                <div class="card">
                    <DashboardCard title="Income (PM)" :amount=income icon="src/assets/images/icons/expenses.png" />
                </div>

                <div class="card">
                    <DashboardCard title="Expenses (PM)" :amount=expenses icon="src/assets/images/icons/budget.png" />
                </div>

                <div class="card">
                        <DashboardCard title="Total Balance" :amount=balance icon="src/assets/images/icons/bill.png" />
                </div>
            </div>

            <div class="main card">
                <SpendingChart />
            </div>
        </div>

        <div class="right card">
                <TransactionList @balanceUpdated="updateAccountBalance" />
        </div>
    </div>

</template>

<style scoped>

.dashboard{
  display:grid;

  grid-template-columns: 5fr 3fr;
  grid-gap: 2rem;

  height: 80vh;

  overflow-y: scroll;
  -ms-overflow-style: none;  /* IE and Edge */
    scrollbar-width: 0px;  /* Firefox */
}


::-webkit-scrollbar {
  display: none;
}

@media (max-width: 1280px) {
    .dashboard{
        grid-template-columns: 1fr;
    }
}

.dashboard > div{
    padding: 0px;
}

.top {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    grid-gap: 3rem;

    height: 17.5%;
}

.top > .card{
    padding: 0px;
}

.main {
    margin-top: 2rem;

    padding: 0px;

    height: calc(82.5% - 2rem);
}

.bar-chart{
    position: relative;
    height: 100%;
}

</style>