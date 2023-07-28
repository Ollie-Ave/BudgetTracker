<script setup>

import LeftContent from '@/components/login/LeftContent.vue';
import SignIn from '@/components/login/SignIn.vue';

</script>

<template>

<div class="login">
  <div class="left">
      <LeftContent />
  </div>
  <div class="right">
      <SignIn />
  </div>
</div>

</template>

<style scoped>

.login{
  display: grid;
  grid-template-columns: 5fr 8fr;
}

.left{
  background: linear-gradient(var(--colour-primary-200), var(--colour-primary-300));

  width: 100%;
  height: 100vh;

  display: flex;
  justify-content: center;
  align-items: center;
  text-align: center;
}

.right{
  width: 100%;
  height: 100vh;

  display: flex;
  justify-content: center;
  align-items: center;
  text-align: center;
}

</style>
