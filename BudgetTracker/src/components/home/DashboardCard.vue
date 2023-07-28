<script setup>
import { computed } from 'vue';
import formatCurrency from '@/sharedLogic/formatCurrency';

const props = defineProps(['title', 'amount', 'icon']);

const formattedAmount = computed(() => formatCurrency(props.amount));
const ammountIsPositive = computed(() => props.amount > 0 ? 'positive' : 'negative');

</script>

<template>
    <div class="container">
        <div class="container-text">
            <div class="title text-muted">
                <div class="dot"></div>
                <h4>{{ title }}</h4>
            </div>
    
            <p :class=ammountIsPositive>{{ formattedAmount }}</p>
        </div>

        <div class="img">
            <img :src=icon alt="icon" />
        </div>
    </div>
</template>

<style scoped>
.container {
    position: relative;
    overflow: hidden;

    height: 100%;
}

.container-text{
    margin: 1.5rem;
}

.title{
    display: flex;
    align-items: center;
}

.title > h4 {
    margin: 0px;
}

.dot{
    height: 0.5rem;
    aspect-ratio: 1/1;
    border-radius: 50%;

    margin-right: 0.5rem;
    background-color: var(--colour-primary-100);
}

.container > p {
    font-size: 1rem;
    margin-left: 1rem;
}
.img{
    position: absolute;
}
img {
    width: 100px;
    aspect-ratio: 1/1;

    
    position: relative;
    bottom: 3rem;
    left: 7rem;

    transform: rotate(-10deg);
}
</style>