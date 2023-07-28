<script setup>
import { ref } from 'vue';

import { useAccountData } from '@/stores/accountData';
import { useLoginStore } from '@/stores/loggedIn';
import { useApiRoutes } from '@/stores/apiRoutes';

import axios from 'axios';

import { Line } from 'vue-chartjs';
import { Chart as ChartJS, Title, Tooltip, Legend, PointElement, LineElement, CategoryScale, LinearScale } from 'chart.js';
ChartJS.register(Title, Tooltip, Legend, PointElement, LineElement, CategoryScale, LinearScale)

const accountDataStore = useAccountData();
const loggedInStore = useLoginStore();
const apiRoutesStore = useApiRoutes();

const accountData = await accountDataStore.getAccountData(loggedInStore.apiKey);

const colour_primary_300 = '#fb6a72';
const colour_success = '#00bc4b';
const colour_warning = '#f5a623';
const dot_colour = '#000';

async function getAccountExpenses(uid) {
    try {
        const response = await axios.get(`${apiRoutesStore.dayTotalExpensesUrl}/${uid}?apiKey=${loggedInStore.apiKey}`);
        return response.data.reverse();
    }
    catch (error) {
        console.log(error)
        // await handleApiError(error);

        return 0;
    }
}

async function getAccountIncome(uid) {
    try {
        const response = await axios.get(`${apiRoutesStore.dayTotalIncomeUrl}/${uid}?apiKey=${loggedInStore.apiKey}`);
        return response.data.reverse();
    }
    catch (error) {
        console.log(error)
        // await handleApiError(error);

        return 0;
    }
}

async function getAccountDifferences(uid) {
    try {
        const response = await axios.get(`${apiRoutesStore.dayTotalDifferenceUrl}/${uid}?apiKey=${loggedInStore.apiKey}`);
        return response.data.reverse();
    }
    catch (error) {
        console.log(error)
        // await handleApiError(error);

        return 0;
    }
}

const ExpensesData = {
    label: 'Outgoing',
    backgroundColor: dot_colour,
    borderColor: colour_primary_300,
    data: await getAccountExpenses(accountData.uid)
};

const incomeData = {
    label: 'Incoming',
    backgroundColor: dot_colour,
    borderColor: colour_success,
    data: await getAccountIncome(accountData.uid)
}

const differenceData = {
    label: 'Difference',
    backgroundColor: dot_colour,
    borderColor: colour_warning,
    data: await getAccountDifferences(accountData.uid)
}

function getLabels(){
    var labels = [];
    var today = new Date();
    for (var i = 0; i < 7; i++) {
        // Generate the date for each label by subtracting the number of days from the current date
        var date = new Date(today.getFullYear(), today.getMonth(), today.getDate() - i);
        labels.push(date.toLocaleDateString('eb-gb', {weekday: 'long'}));
    }

    return labels.reverse();
}



const chartData = ref({
    labels: getLabels(),
    datasets: [
        ExpensesData,
        incomeData,
        differenceData
    ]
});

const chartOptions = ref({
    responsive: true,
    maintainAspectRatio: false,
    elements: {
        line: {
            tension: 0.2
        }
    }
});

</script>

<template>
    <div class="container">
        <div class="container-inner">
            <h2>Spending this week</h2>
            <hr>
            <div class="bar-chart">
                <Line :data="chartData" :options="chartOptions" />
            </div>

        </div>
    </div>
</template>


<style scoped>

.container{
    display: inline-block;
    height: 100%;
    width: 100%;
}

.container-inner{
    margin: 1rem;
    height: calc(100% - 2rem);
}

h2{
    margin-bottom: 1.25rem;
}

.bar-chart{
    height: 85%;
    display: block;
    position: relative;
}
</style>

