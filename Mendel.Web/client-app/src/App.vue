<script setup lang="ts">
import { ref, onMounted, nextTick, onUnmounted, computed } from 'vue';
import { RouterView } from 'vue-router';
import NavBar from '@/navbar/NavBar.vue';

const isMenuOpen = ref(false);

const toggleMenu = () => {
  isMenuOpen.value = !isMenuOpen.value;
};

const toggleButton = ref<HTMLButtonElement | null>(null);
const buttonHeight = ref(0);
const windowHeight = ref(window.innerHeight);
const mainMaxHeight = ref('100vh');

const calculateMaxHeight = () => {
  if (toggleButton.value) {
    buttonHeight.value = toggleButton.value.offsetHeight;
    mainMaxHeight.value = `calc(100vh - ${buttonHeight.value}px)`;
  } else {
    buttonHeight.value = 0;
  }
};

const closeMenu = () => {
  isMenuOpen.value = false;
};

const showModal = ref<boolean>(true);

onMounted(() => {
  nextTick(() => {
    calculateMaxHeight();

    const resizeObserver = new ResizeObserver(() => {
      windowHeight.value = window.innerHeight;
      calculateMaxHeight();
    });

    resizeObserver.observe(document.body);

    // Clean up the observer when the component is unmounted
    onUnmounted(() => {
      resizeObserver.disconnect();
    });
  });
});
</script>

<template>
  <div class="lg:grid lg:grid-rows-9 lg:grid-cols-9 h-screen">
    <div
      id="main-window"
      class="box flex flex-col h-screen lg:h-full lg:row-start-2 lg:row-span-7 lg:col-start-2 lg:col-span-5 bg-[#b2c0a7]"
      :style="{ bottom: `calc(100vh - ${buttonHeight}px)` }"
    >
<!--      <div v-if="showModal" class="modal-overlay">-->
<!--        TEST MODAL-->
<!--      </div>-->
      <div class="bg-sagegreen flex justify-between px-2">
        <div class="text-white">🧬 MENDEL.exe</div>
        <div class="flex items-center gap-1">
          <div class="h-[15px] w-[15px] bg-[#555243] border-2 border-[#4094A1]"></div>
          <div class="h-[15px] w-[15px] bg-[#555243] border-2 border-[#65A4AD]"></div>
          <div class="h-[15px] w-[15px] bg-[#555243] border-2 border-[#8FB5BC]"></div>
        </div>
      </div>
      <div
        class="bg-[#ECEEF0] grow overflow-scroll mb-11 p-2 border-2 border-[#707070] mx-2 mt-1 lg:m-2 lg:mb-2"
      >
        <RouterView />
      </div>
    </div>
    <div class="flex flex-col lg:hidden">
      <Transition name="slide-up">
        <div
          id="mobile-menu"
          class="absolute w-full"
          :style="{ bottom: `calc( ${buttonHeight}px)` }"
          v-if="isMenuOpen"
        >
          <NavBar @close-menu="closeMenu" />
        </div>
      </Transition>
      <button
        class="bg-sagegreen h-[45px] fixed bottom-0 w-full text-white"
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
    </div>
    <div
      class="hidden box lg:flex lg:flex-col lg:row-start-3
       lg:row-span-3 lg:col-start-7 lg:col-span-2 bg-[#b2c0a7] ml-5"
    >
      <div class="bg-sagegreen text-center px-2">
        <div class="text-white ">Menu</div>
      </div>
      <div class="bg-[#ECEEF0] grow p-2 border-2 border-[#6c9085] m-2">
        <NavBar @close-menu="closeMenu" />
      </div>
    </div>
  </div>
</template>

<style>
main,
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

.slide-up-enter-active,
.slide-up-leave-active {
  transition: transform 0.3s ease;
}

.slide-up-enter-from,
.slide-up-leave-to {
  transform: translateY(100%);
}

.modal-overlay {
  @apply absolute z-10 border-2 bg-white w-full h-full place-self-center;
}
</style>