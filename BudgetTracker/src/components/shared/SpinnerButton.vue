<script setup>
import { ref } from 'vue'

import Spinner from '@/components/shared/Spinner.vue';

const props = defineProps(['clickCallback', 'buttonText']);
const emit = defineEmits(['clicked']);

const showButton = ref(true);

function clicked() {
    showButton.value = false;
    
    props.clickCallback();
    
    emit('clicked');
}

function resetState() {
    showButton.value = true;
}

defineExpose({ resetState });
</script>

<template>
    <button @click.prevent="clicked" class="btn btn-proceed" v-if="showButton">{{ buttonText }}</button>
    <Spinner v-if="!showButton" />
</template>

<style scoped>
button{
    padding: 0.5rem 1.25rem;
}
</style>