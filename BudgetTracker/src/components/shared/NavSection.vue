<script setup>
</script>

<template>

    <div class="nav card">
        <div class="nav-link">
            <router-link to="/">
                <i class="bi bi-window-desktop icon"></i>
                Dashboard
            </router-link>
        </div>
        <div class="nav-link">
            <router-link to="/bills-and-payments">
                <i class="bi bi-credit-card"></i>
                Bills & Payments
            </router-link>
        </div>
        <div class="nav-link">
            <router-link to="/expenses">
                <i class="bi bi-receipt-cutoff"></i>
                Expenses
            </router-link>
        </div>
        <div class="nav-link">
            <router-link to="/income">
                <i class="bi bi-cash-stack"></i>
                Income
            </router-link>
        </div>
        <div class="nav-link">
            <router-link to="/settings">
                <i class="bi bi-gear"></i>
                Settings
            </router-link>
        </div>
    </div>

</template>

<style scoped>

    .nav{
        display: flex;
        flex-direction: column;

        height: 100%;
        padding: 0px;
    }

    .nav-link:first-child{
        margin-top: 2rem;
    }

    .nav-link{
        margin: 1rem 2rem;
    }

    a{
        display: flex;
        align-items: center;
        justify-content: left;
        color: var(--colour-text-white);
        text-decoration: none;

        transition: ease 0.2s;
    }

    .nav-link:hover > a{
        cursor: pointer;
        
        color: var(--colour-primary-100);
    }

    i {
        font-size: 1.4rem;
        margin: 0rem 1rem;
    }
</style>