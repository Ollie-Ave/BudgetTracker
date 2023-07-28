<script setup>
import { ref } from 'vue';
import { useApiRoutes } from '@/stores/apiRoutes';
import { useLoginStore } from '@/stores/loggedIn';
import { useAccountData } from '@/stores/accountData';

const apiRoutes = useApiRoutes();
const loggedIn = useLoginStore();
const accountDataStore = useAccountData();

const accountData = await accountDataStore.getAccountData(loggedIn.apiKey);

const firstName = ref(accountData.firstName);
const profilePictureUrl = ref(`${apiRoutes.profilePictureUrl}/${accountData.uid}?apiKey=${loggedIn.apiKey}`);
</script>

<template>
    <div class="banner">
        <div class="left">
            <img id="logo"  src="@/assets/images/icons/budget.png" alt="Budget Tracker Logo" />
            <h2>Budget Tracker</h2>
        </div>

        <div class="right">
            <img id="profile-pic" :src=profilePictureUrl>
            <p>Hi, {{ firstName }}</p>
            <button class="logout" @click="loggedIn.logout()">
              <i class="bi bi-box-arrow-right" @click="loggedIn.logout()"></i>
            </button>
        </div>
    </div>
</template>

<style scoped>

.banner {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 2rem;
  height: 10vh;

  box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);

  background-color: var(--colour-surface-200);
}

.banner > * {
  height: 100%;

  display: flex;
  align-items: center;
  justify-content: space-between;
}

#logo{
  height: 40%;
  aspect-ratio: 1/1;

  margin-right: 1rem;
}

.right{
  height: 100%;

  display: flex;
  align-items: center;
  justify-content: space-between;
}

.right > * {
  margin: 0rem 0.5rem; 
} 

.right > p {
  font-size: 1rem;
}

#profile-pic{
  height: 50%;
  aspect-ratio: 1/1;

  margin-right: 1rem;
  border-radius: 100%;
  object-fit: cover;
}

.logout {
  background-color: transparent;
  border: none;
  margin-top: 1.5px;
}

i {
  font-size: 1.5rem;
  color: var(--colour-text-white);
  transition: ease 0.2s;
}

i:hover{
  cursor: pointer;
  color: var(--colour-primary-100);
}

</style>