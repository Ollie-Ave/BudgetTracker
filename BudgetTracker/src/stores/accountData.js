import { ref } from 'vue';

import { defineStore } from 'pinia'

import { useApiRoutes } from '@/stores/apiRoutes';

import handleApiErrors from '@/sharedLogic/handleApiErrors';

import axios from 'axios';

export const useAccountData = defineStore('accountData', () => {
    const apiRoutes = useApiRoutes();

    const accountData = ref();

    async function getAccountData(apiKey) {
        if (accountData.value == null) {
            try {
                const response = await axios.get(`${apiRoutes.accountUrl}?apiKey=${apiKey}`);
                
                accountData.value = response.data;
            }
            catch (error) {
                await handleApiErrors(error);
            }
        }

        return accountData.value;
    }

    function updateBalance(newBalance) {
        accountData.value.balance = newBalance;
    }

    function wipeAccountData() {
        accountData.value = null;
    }

    return { getAccountData, wipeAccountData, updateBalance };
});