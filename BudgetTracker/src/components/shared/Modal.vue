<script setup>
const props = defineProps(['showModal', 'transition']);
</script>

<template>
    <transition :name="transition" mode="out-in">
        <div class="modal-background" :class="{ 'play-animation' : transition != undefined && transition != 'none' }" v-show="props.showModal">
            <div class="card">
                <slot>
                </slot>
            </div>
        </div>
    </transition>
</template>

<style scoped>

.fade-enter-from {
    display: flex;
    opacity: 0;
}

.fade-leave-to {
  opacity: 0;
  display: none;
}

.modal-background{
    position: fixed;
    top: 0px;
    left: 0px;

    width: 100vw;
    height: 100vh;

    background-color: #00000057;

    display: flex;
    justify-content: center;
    align-items: center;

    z-index: 100;
}

.modal-background > * {
    text-align: center;
}

.play-animation {
    transition: 0.2s ease;
}
</style>