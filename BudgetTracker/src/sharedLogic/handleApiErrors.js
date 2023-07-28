import { toast } from 'vue3-toastify';
import router from '@/router';
import { useLoginStore } from '@/stores/loggedIn';
import { useAccountData } from '@/stores/accountData';

async function handleApiErrors(error) {
    const loginStore = useLoginStore();
    const accountDataStore = useAccountData();

    await router.push('/login');
                
    accountDataStore.wipeAccountData();
    loginStore.logout();

    if (error.code == 'ERR_NETWORK') {
        toast.error('Unable to connect to the server, you have been logged out. Please try again later.');
    }
    else {
        console.error(error);


        if (error?.response?.status == 401) {
            toast.error('Your session has expired. Please log in again.');
        }
        else {
            toast.error('Something went wrong. Please try again later.');
        }
    }
}

export default handleApiErrors;