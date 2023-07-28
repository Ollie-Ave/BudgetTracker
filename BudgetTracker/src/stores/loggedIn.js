import { ref } from 'vue'
import { defineStore } from 'pinia'
import router from '@/router';
import { toast } from 'vue3-toastify';
import { useAccountData } from '@/stores/accountData';

export const useLoginStore = defineStore('loggedIn', () =>
{
  const accountData = useAccountData();
  const status = ref(localStorage.getItem('loggedIn') ?? false);
  const apiKey = ref(localStorage.getItem('apiKey') ?? '');

  function login(key)
  {
    status.value = 'true';
    apiKey.value = key;

    localStorage.setItem('loggedIn', status.value);
    localStorage.setItem('apiKey', apiKey.value);
  }

  async function logout()
  {
    status.value = 'false';
    apiKey.value = '';
    
    accountData.wipeAccountData();

    localStorage.setItem('loggedIn', status.value);
    localStorage.setItem('apiKey', apiKey.value);
    
    await router.push('/login');

    toast.error('You have been logged out.');
  }

  return { status, apiKey, login, logout };
});