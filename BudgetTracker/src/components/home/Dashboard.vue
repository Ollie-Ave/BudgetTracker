<script setup>
import { ref, watch } from 'vue';

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

const accountData = await accountDataStore.getAccountData(loggedInStore.apiKey);

async function getAccountBalance() {
    const accountData = await accountDataStore.getAccountData(loggedInStore.apiKey);
    balance.value = accountData.balance;
}

const balance = ref(0.0);
const expenses = ref(0.0);

await getAccountBalance();
await getAccountExpenses(accountData.uid);

const income = ref(await getAccountIncome(accountData.uid));

watch(balance, async () => {
    await getAccountExpenses(accountData.uid);
});

async function getAccountExpenses(uid) {
    try {
        const response = await axios.get(`${apiRoutesStore.totalExpensesUrl}/${uid}?apiKey=${loggedInStore.apiKey}`);
        expenses.value = response.data;
    }
    catch (error) {
        await handleApiError(error);

        return 0;
    }
}

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
                <TransactionList @balanceUpdated="getAccountBalance" />
        </div>
    </div>

</template>

<style scoped>

.dashboard{
  display:grid;

  grid-template-columns: 5fr 3fr;
  grid-gap: 2rem;
  
  height: 100%;
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