<script lang="ts" setup>
import { ref, watch, onMounted } from 'vue';
import { useSpeciesStore } from '@/admin/species.store';
import { RouterLink } from 'vue-router';
import DropdownMenu from '@/components/dropdown-menu/DropdownMenu.vue';

const speciesStore = useSpeciesStore();
</script>

<template>
  Todo: Update Event, Release Date
  <div class="p-2">
    <div class="flex gap-2">
      <RouterLink :to="{ name: 'Admin' }" class="button">Main Admin</RouterLink>
    </div>
    <h2>Species List</h2>
    <div class="border-2 border-black p-2 rounded mb-2">
      Count: {{ speciesStore.species?.length }}
      <div v-if="speciesStore.species">
        <ul>
          <li v-for="(item, index) in speciesStore.species" :key="index" class="border pl-2">
            {{ item.id }} - {{ item.name }}
          </li>
        </ul>
      </div>
    </div>

    <div class="border-2 border-black p-2 mb-2">
      <h2>Add Species</h2>
      <form @submit.prevent="speciesStore.addSpecies" class="flex flex-col gap-2">
        <label for="name">Name:</label>
        <input
          id="name"
          v-model="speciesStore.newSpecies.name"
          type="text"
          placeholder="Species Name"
        />

        <label for="event">Event:</label>
        <DropdownMenu
          :menu="speciesStore.eventNames"
          @option-selected="speciesStore.setEvent"
        />

        <label for="capUrl">Capsule Url:</label>
        <input
          id="capUrl"
          v-model="speciesStore.newSpecies.capsuleImage"
          type="text"
          placeholder="Capsule Url"
        />

        <label for="releaseDate">Release Date:</label>
        <input
          id="releaseDate"
          type="date"
          v-model="speciesStore.newSpecies.releaseDate"
          clearable
          variant="outlined"
        />

        <button>Submit</button>
      </form>

    </div>

<!--    <div class="border-2 border-black p-2 mb-2">-->

<!--      <form @submit.prevent="speciesStore.addGenes" class="flex flex-col gap-2">-->
<!--        <label for="speciesId">SpeciesId:</label>-->
<!--        <input-->
<!--          id="speciesId"-->
<!--          v-model="speciesStore.newGenes.speciesId"-->
<!--          type="text"-->
<!--          placeholder="Species Id"-->
<!--        />-->

<!--        <label for="genes">Gene Json:</label>-->
<!--        <input-->
<!--          id="genes"-->
<!--          v-model="speciesStore.newGenes.geneJson"-->
<!--          type="text"-->
<!--          placeholder="Genes"-->
<!--        />-->

<!--        <button>Submit</button>-->

<!--        {{ speciesStore.newGenes }}-->


<!--      </form>-->
<!--    </div>-->
  </div>
</template>