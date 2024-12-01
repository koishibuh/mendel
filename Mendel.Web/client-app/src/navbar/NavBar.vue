<script lang="ts" setup>
import { RouterLink } from 'vue-router';
import { useGlobalStore } from '@/common/global.store';
import { ref } from 'vue';

const store = useGlobalStore();

const emit = defineEmits(['close-menu']);

const closeMenu = () => {
  emit('close-menu');
};

const username = ref<string>('');
</script>

<template>
  <div class="p-2 flex flex-col gap-2">
    <RouterLink @click="closeMenu" :to="{ name: 'Review' }" class="button">Click</RouterLink>
    <!--    <RouterLink @click="closeMenu" :to="{ name: 'Checklist' }" class="button w-full mb-2"-->
    <!--      >Checklist</RouterLink-->
    <!--    >-->

    <div v-if="store.storedUser">
      <div>Welcome Back</div>
      <div>{{ store.storedUser }}</div>
      <div @click="store.resetUser" class="underline hover:bg-lightgreen">Change Username</div>
    </div>
    <div v-else>
      <div id="scientistName" class="p-2">
        <label for="scientist-name">Save your lab name:</label>
        <form @submit.prevent="store.setUser" class="flex flex-col gap-2">
          <input id="username" class="w-full" type="text" v-model="username" />
          <button @click="store.setUser(username)" class="button w-full">Submit</button>
        </form>
      <span v-if="store.message"  class="bg-red-500 px-2">{{ store.message }}</span>
      </div>
    </div>

  </div>


</template>