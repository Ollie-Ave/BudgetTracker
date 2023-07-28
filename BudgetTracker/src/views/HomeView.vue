<script setup>
import TopBanner from '@/components/shared/TopBanner.vue';
import NavSection from '@/components/shared/NavSection.vue';

import Dashboard from '@/components/home/Dashboard.vue';
</script>

<template>
    <Suspense>
        <TopBanner />
    </Suspense>

    <main>
        <div class="left">
        <NavSection />
        </div>

        <div class="right">
            <Suspense>
              <Dashboard />
            </Suspense>
        </div>
    </main>
</template>

<style scoped>

main{
  display:grid;

  grid-template-columns: 0.7fr 3fr;
  grid-gap: 2rem;
  
  margin: 3rem 3rem;

  height: 80vh;
}

main > div{
  height:100%;
}

</style>