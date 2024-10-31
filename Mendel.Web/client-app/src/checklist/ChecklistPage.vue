<script lang="ts" setup>
import { onMounted, ref, computed } from 'vue';
import { type IGenotype, useChecklistStore } from '@/checklist/checklist.store';
import ChecklistPage from '@/checklist/ChecklistPage.vue';

onMounted(() => {
  // get list of holiday species from backend
});

const store = useChecklistStore();

const scientistName = ref<string>('');

const genes = (geneIds: number[]) : IGenotype[] => {
return store.eventSpecies[0].genes.filter(x => geneIds.includes(x.id));
}

</script>

<template>
  <div class="p-2">
    <div class="m-2">
      <label for="scientist-name">Scientist Name</label>
      <div class="flex gap-2">
        <input id="scientist-name" class="w-full" type="text" v-model="scientistName" />
        <input type="button" value="Submit" />
      </div>
    </div>

    <div class="flex flex-col">
      <p>{{ store.eventSpecies[0].event }}</p>
      <p>{{ store.eventSpecies[0].speciesName }}</p>

      <div>Missing</div>

      <div class="md:grid md:grid-cols-3 gap-2">
        <div v-for="(item, index) in store.eventSpecies[0].portraits" :key="index" class="border-2 border-black mb-2">
          <div class="border-2 w-full"><img :src="item.adult" alt=""/></div>
          <div class="border-2">Count: </div>
          <ul>
            <li v-for="gene in genes(item.geneIds)" :key="gene.id">
              {{gene.trait}} - {{gene.alleles}} - {{gene.description}}
            </li>
          </ul>

        </div>
      </div>

      <div>Completed</div>
    </div>
  </div>
</template>