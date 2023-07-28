import { defineStore } from 'pinia'

export const useApiRoutes = defineStore('apiRoutes', () =>
{
    const baseUrl = 'https://localhost:7045/api';
    const loginUrl = `${baseUrl}/login`;
    const accountUrl = `${baseUrl}/account`;
    const profilePictureUrl = `${baseUrl}/profilePicture`;
    const transactionsUrl = `${baseUrl}/transactions`;
    const totalExpensesUrl = `${baseUrl}/totalExpenses`;
    const totalIncomeUrl = `${baseUrl}/totalIncome`;
    const dayTotalUrl = `${baseUrl}/dayTotal`;
    const dayTotalIncomeUrl = `${dayTotalUrl}/income`;
    const dayTotalExpensesUrl = `${dayTotalUrl}/expense`;
    const dayTotalDifferenceUrl = `${dayTotalUrl}/difference`;

    return {
        baseUrl,
        loginUrl,
        accountUrl,
        profilePictureUrl,
        totalExpensesUrl,
        totalIncomeUrl,
        transactionsUrl,
        dayTotalUrl,
        dayTotalIncomeUrl,
        dayTotalExpensesUrl,
        dayTotalDifferenceUrl
    }
});