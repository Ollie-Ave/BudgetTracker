<script setup>
</script>

<template>

<div>
    <svg width="2rem" height="2rem" viewBox="0 0 66 66" xmlns="http://www.w3.org/2000/svg" class="spinner">
        <circle fill="none" stroke-width="6" stroke-linecap="round" cx="33" cy="33" r="30" class="path" style="stroke: var(--colour-primary-300) !important;"></circle>
    </svg>
</div>

</template>

<style scoped>
.spinner{
    animation: rotator 1.4s linear infinite;
}

@keyframes rotator {
    0%{
        transform: rotate(0deg);
    }    
    100%{
        transform: rotate(1turn);
    }    
}

.path{
    stroke-dasharray: 187;
    stroke-dashoffset: 0;
    transform-origin: center;
    animation: dash 1.4s ease-in-out infinite, colors 5.6s ease-in-out infinite;
}

@keyframes dash{
    0%{
        stroke-dashoffset: 187;
    }
    50%{
        stroke-dashoffset: 46.75;
        transform: rotate(135deg);
    }
    100%{
        stroke-dashoffset: 187;
        transform: rotate(360deg);
    }
}

</style>