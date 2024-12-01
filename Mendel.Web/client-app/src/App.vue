<script setup lang="ts">
import { ref, onMounted, onUnmounted, onBeforeUnmount } from 'vue';
import { RouterView } from 'vue-router';
import NavBar from '@/navbar/NavBar.vue';
import { useGlobalStore } from '@/common/global.store';
import type { IMendelSession } from '@/common/model/IMendelSession';

onMounted(() => {
  try {
    const storedSession = localStorage.getItem('mendel');
    if (storedSession) {
      store.loadStoredSession(JSON.parse(storedSession) as IMendelSession);
    }
  } catch (error) {
    console.log('Error accessing LocalStorage', error);
  }

  window.addEventListener('resize', closeMenu);
});

onBeforeUnmount(() => {
  try {
    store.saveToLocalStorage();
  } catch (error) {
    console.log('Error saving to LocalStorage', error);
  }
});

onUnmounted(() => {
  window.removeEventListener('resize', closeMenu);
});

const store = useGlobalStore();

const isMenuOpen = ref(false);
const toggleButton = ref<HTMLButtonElement | null>(null);

const toggleMenu = () => {
  isMenuOpen.value = !isMenuOpen.value;
};

const closeMenu = () => {
  isMenuOpen.value = false;
};
</script>

<template>
  <div class="flex md:justify-center lg:items-center h-dvh">
    <div class="lg:flex space-x-5">
      <div class="w-screen md:w-[43rem] lg:h-[83vh] flex flex-col h-full box">
        <div
          class="bg-sagegreen px-3 py-1 fixed w-full md:w-[43rem] top-0 lg:static flex items-center z-50"
        >
          <div class="flex-shrink-0 text-white">🧬 MENDEL.exe</div>

          <button
            class="text-white h-[45px] w-full text-end lg:hidden"
            ref="toggleButton"
            @click="toggleMenu"
            aria-expanded="false"
            aria-controls="mobile-menu"
          >
            <svg
              class="w-6 h-6 inline-block"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M4 6h16M4 12h16m-7 6h7"
              ></path>
            </svg>
            Menu
          </button>

          <div class="hidden lg:flex items-center gap-1 justify-end w-full">
            <div class="h-[15px] w-[15px] bg-[#555243] border-2 border-[#4094A1]"></div>
            <div class="h-[15px] w-[15px] bg-[#555243] border-2 border-[#65A4AD]"></div>
            <div class="h-[15px] w-[15px] bg-[#555243] border-2 border-[#8FB5BC]"></div>
          </div>
        </div>

        <Transition name="slide-down">
          <div
            id="mobile-menu"
            class="fixed w-full md:w-[43rem] lg-hidden py-5 bg-lightgreen z-40 border-4 border-sagegreen mt-[45px]"
            v-if="isMenuOpen"
          >
            <NavBar @close-menu="closeMenu" />
          </div>
        </Transition>

        <div
          class="grow flex gap-2 flex-col overflow-auto bg-offWhite p-1 sm:px-5 pt-[48px] md:py-2 md:px-0"
        >
          <RouterView />
        </div>
      </div>

      <div class="hidden lg:flex w-60 h-[40vh] flex-col mt-8 box">
        <div class="bg-sagegreen text-offWhite p-1 text-center">Menu</div>
        <div class="bg-offWhite flex-grow p-4">
          <NavBar @close-menu="closeMenu" />
        </div>
      </div>
    </div>
  </div>
</template>

<style>
.box {
  box-shadow: rgba(0, 0, 0, 0.5) 2.4px 2.4px 3.2px;
}

#nav-box {
  box-shadow:
    4px 0 4px -2px rgba(0, 0, 0, 0.5),
    0 4px 4px -2px rgba(0, 0, 0, 0.5);
}

#title-bar {
  box-shadow:
    0 -4px 4px -2px rgba(0, 0, 0, 0.5),
    -4px 0 4px -2px rgba(0, 0, 0, 0.5),
    4px 0 4px -2px rgba(0, 0, 0, 0.5);
}

#bottom-box {
  box-shadow:
    0 4px 4px -2px rgba(0, 0, 0, 0.5),
    -4px 0 4px -2px rgba(0, 0, 0, 0.5),
    4px 0 4px -2px rgba(0, 0, 0, 0.5);
}

.slide-down-enter-active,
.slide-down-leave-active {
  transition: transform 0.4s ease;
}

.slide-down-enter-from,
.slide-down-leave-to {
  transform: translateY(-100%);
}
</style>