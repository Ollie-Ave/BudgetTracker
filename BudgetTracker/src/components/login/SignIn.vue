<script setup>
import { ref } from 'vue';
import { useLoginStore } from '@/stores/loggedIn';
import  { useApiRoutes } from '@/stores/apiRoutes';

import axios from 'axios';
import { toast } from 'vue3-toastify';

import router from '@/router';
import Spinner from '@/components/shared/Spinner.vue';
import SpinnerButton from '@/components/shared/SpinnerButton.vue';

const spinnerId = ref('submit_spinner');
const submitId = ref('submit');

const email = ref('');
const password = ref('');

const loggedIn = useLoginStore();
const apiRoutes = useApiRoutes();

const submitButton = ref(null);

async function PerformLogin() {
    try {
        const response = await axios.post(
            apiRoutes.loginUrl,
            {
                email: email.value,
                password: password.value
            });

        loggedIn.login(response.data);
        router.push({ path: '/' });
    }
    catch (error) {
        if (error.response.status = 401) {
            toast.error('Either your username or password were incorrect. Please check these credentials and try again.');

            submitButton.value.resetState();
        }
        else {
            toast.error('An unexpected error occured, please try again.');

            submitButton.value.resetState();
        }
    }
}

async function submitted()
{
    if (email.value === '' || password.value === '')
    {
        submitButton.value.resetState();
        
        toast.error('Please ensure you have provided both an email and password.');
    }
    else
    {
        await PerformLogin();
    }
}

</script>

<template>

<div class="sign-in card card-rounded">

    <h2>Log In</h2>
    <hr>

    <form>
        <div class="input-field">
            <h3>Email:</h3>
            <input v-model="email" type="text" class="text-input" placeholder="johndoe@email.com" autocomplete="off">
        </div>

        <div class="input-field">
            <h3>Password:</h3>
            <input v-model="password" type="password" class="text-input" placeholder="password" >
        </div>
        
        <div class="input-submit">
            <SpinnerButton ref="submitButton" buttonText="Submit" :clickCallback="submitted" />
        </div>
    </form>

</div>

</template>

<style scoped>

.sign-in{
    display: flex;
    flex-direction: column;
    
    align-items: center;
    justify-content: center;

    width: 100%;
    max-width: 300px;

    margin: 0rem 1rem;

    box-shadow: 0.5rem 0.5rem 1rem 0.1rem #000;
}

form{
    width: 100%;
    display: flex;
    flex-direction: column;

    align-items: center;
}

h2{
    margin-bottom: 0.3rem;
}

hr{
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
</style>