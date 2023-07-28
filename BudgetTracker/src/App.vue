<script setup>
import 'bootstrap-icons/font/bootstrap-icons.css';
import { RouterView } from 'vue-router';
import 'vue3-toastify/dist/index.css';
</script>

<template>
  <RouterView :key="$route.fullPath" />
</template>

<style scoped>

</style>
