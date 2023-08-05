import { defineStore } from 'pinia'

export const useApiRoutes = defineStore('apiRoutes', () =>
{
    const baseUrl = 'https://localhost:7045/api';
    const loginUrl = `${baseUrl}/login`;
    const accountUrl = `${baseUrl}/account`;
    const profilePictureUrl = `${baseUrl}/profilePicture`;
    const transactionsUrl = `${baseUrl}/transactions`;
    const totalIncomeUrl = `${baseUrl}/totalIncome`;
    const dayTotalUrl = `${baseUrl}/dayTotals`;
    const dayTotalIncomeUrl = `${dayTotalUrl}/income`;
    const dayTotalExpensesUrl = `${dayTotalUrl}/expense`;
    const dayTotalDifferenceUrl = `${dayTotalUrl}/difference`;

    return {
        baseUrl,
        loginUrl,
        accountUrl,
        profilePictureUrl,
        totalIncomeUrl,
        transactionsUrl,
        dayTotalUrl,
        dayTotalIncomeUrl,
        dayTotalExpensesUrl,
        dayTotalDifferenceUrl
    }
});