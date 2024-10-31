<script lang="ts" setup>
import { ref, watch, onMounted } from 'vue';
import { type INoGeneImage, useAdminStore } from '@/admin/admin.store';
import type { IGenotype } from '@/checklist/checklist.store';
import type { IImage } from '@/admin/admin.store';
import { useSpeciesStore } from '@/admin/species.store';
import DropdownMenu from '@/components/dropdown-menu/DropdownMenu.vue';
import { RouterLink } from 'vue-router';
import { useImageGeneStore } from '@/admin/image-gene.store';

const imageGeneStore = useImageGeneStore();
const speciesStore = useSpeciesStore();
const store = useAdminStore();

const speciesName = ref<string>('');
const speciesId = ref<number>(0);

// const convertId = (geneIds: number[][]): IGenotype[] => {
//   return store.speciesGenes.filter((x) => geneIds.some((idArray) => idArray.includes(x.id)));
// };
const imageFile = ref();
const imageUrl = ref();

const filename = ref();
const portraitId = ref<number>(1);

// const imageFiles = ref<File[]>([]);

// function handleImageSelected(event: Event) {
//   const target = event.target as HTMLInputElement;
//   if (target.files) {
//     // Convert FileList to array and store all selected files
//     imageFiles.value = Array.from(target.files);
//   }
// }

// watch(imageFiles, (files) => {
//   if (!files.length) return;
//
//   // Process each file
//   files.forEach((file, index) => {
//     if (!(file instanceof File)) return;
//
//     const fileReader = new FileReader();
//     fileReader.readAsDataURL(file);
//     fileReader.addEventListener('load', () => {
//       // Assuming store.image.image is now an array
//       if (!Array.isArray(store.image.images)) {
//         store.image.images = [];
//       }
//       store.image.images[index] = fileReader.result;
//     });
//   });
// });

const handleJuvenileSelected = (gene: INoGeneImage) => {
  store.selectedJuvenile = gene;
};

const toggleItem = (id: number) => {
  const index = store.selectedAdults.indexOf(id);
  if (index === -1) {
    store.selectedAdults.push(id);
  } else {
    store.selectedAdults.splice(index, 1);
  }
};

const handleGetImageGenes = async () => {
  if (speciesStore.selectedSpecies) {
    await speciesStore.getSpeciesImages(speciesStore.selectedSpecies.id);
  }
};
</script>

<template>
  <div class="m-2">
    <div class="flex gap-2">
      <RouterLink :to="{ name: 'Species' }" class="button">Species</RouterLink>
    </div>


    <h2>Images</h2>
    <div class="border-2 border-black p-2">
      <label for="species-name">Load Species</label>
      <div class="mb-2">
        <div class="flex gap-2">
          <DropdownMenu
            :menu="speciesStore.options"
            @option-selected="imageGeneStore.updateSelectedSpecies"
          />
          <input type="button" value="Submit" @click="imageGeneStore.getImageGenes()" />
        </div>
      </div>

      <div>
        {{ imageGeneStore.selectedSpeciesGenes }}

        <div>Juveniles</div>
        <div>Adults</div>

      </div>




<!--      <button @click="store.getJuvenileNoGenes()">Get Juvenile Images</button>-->
<!--      <button @click="store.addJuvenileToAdult()">Add Juvenile Images</button>-->
<!--      <div class="md:grid md:grid-cols-2 md:gap-2">-->
<!--        <div class="w-full">-->
<!--          JUVENILES-->
<!--          {{ store.selectedJuvenile?.id }}-->
<!--          <ul class="flex flex-wrap checkbox-list" v-if="store.juvenileNoGene">-->
<!--            <li-->
<!--              v-for="(item, index) in store.juvenileNoGene"-->
<!--              :key="index"-->
<!--              class="flex flex-col border-2 w-1/4 p-2"-->
<!--              @click="handleJuvenileSelected(item)"-->
<!--              :class="{ selected: store.selectedJuvenile?.id == item.id }"-->
<!--            >-->
<!--              <span class="checkbox"></span>-->
<!--              <p>{{ item.sex }}</p>-->
<!--              <img :src="item.url" alt="" class="size-fit m-auto" />-->
<!--              <button @click="speciesStore.deleteImage(item.id)">Delete</button>-->
<!--            </li>-->
<!--          </ul>-->
<!--        </div>-->

<!--        <div class="w-full bg-gray-300 p-2 rounded">-->
<!--          <p>Adults - {{ store.geneIdSets }}</p>-->
<!--          <p>Gene Count - {{ store.filteredGenes.length }}</p>-->
<!--          <div class="flex gap-2">-->
<!--            <input type="checkbox" v-model="store.filterActive" />-->
<!--            <label>Display Only Missing Images</label>-->
<!--          </div>-->
<!--          <ul class="checkbox-list">-->
<!--            <li-->
<!--              v-for="item in store.filteredGenes"-->
<!--              :key="item.imageId"-->
<!--              @click="toggleItem(item.imageId)"-->
<!--              :class="{ selected: store.selectedAdults.includes(item.imageId) }"-->
<!--            >-->
<!--              <div class="w-full">-->
<!--                <div class="flex justify-between bg-gray-500 gap-2 p-2">-->
<!--                  <span class="checkbox"></span>-->
<!--                  <p>ImageGene Id: {{ item.imageId }}</p>-->
<!--                  <div>{{ item.sex }}</div>-->
<!--                </div>-->
<!--                <div class="grid grid-cols-3">-->
<!--                  <div class="bg-r p-2">-->
<!--                    <p class="bg-yellow-300">Juvenile</p>-->
<!--                    <div v-if="item.juvenileUrl" class="bg-b grow items-center justify-center">-->
<!--                      <img :src="item.juvenileUrl" class="max-w-full max-h-full object-contain" />-->
<!--                    </div>-->
<!--                    <div v-else class="text-center grow">-->
<!--                      <p>Image Not Found</p>-->
<!--                    </div>-->
<!--                    <div class="bg-yellow-300">-->
<!--                      <button class="" @click="speciesStore.deleteImage(item.imageId)">-->
<!--                        Delete-->
<!--                      </button>-->
<!--                    </div>-->
<!--                  </div>-->
<!--                  <div class="bg-g p-2">-->
<!--                    <p>Adult</p>-->
<!--                    <div v-if="item.adultUrl">-->
<!--                      <img :src="item.adultUrl" />-->
<!--                    </div>-->
<!--                    <div v-else>-->
<!--                      <p>Image Not Found</p>-->
<!--                    </div>-->
<!--                    <button>Delete</button>-->
<!--                  </div>-->
<!--                  <div class="bg-b p-2">-->
<!--                    <p>Genes</p>-->
<!--                  </div>-->
<!--                </div>-->
<!--              </div>-->

<!--              &lt;!&ndash;              <div class="flex gap-2 border-black border-2">&ndash;&gt;-->
<!--              &lt;!&ndash;                <span class="checkbox"></span>&ndash;&gt;-->
<!--              &lt;!&ndash;                {{ item.imageId }}&ndash;&gt;-->
<!--              &lt;!&ndash;                <p>{{ item.sex }}</p>&ndash;&gt;-->
<!--              &lt;!&ndash;                <div class="flex">&ndash;&gt;-->
<!--              &lt;!&ndash;                  <div>&ndash;&gt;-->
<!--              &lt;!&ndash;                    <p>Juvenile:</p>&ndash;&gt;-->
<!--              &lt;!&ndash;                    <div v-if="item.juvenileUrl">&ndash;&gt;-->
<!--              &lt;!&ndash;                      <img :src="item.juvenileUrl" />&ndash;&gt;-->
<!--              &lt;!&ndash;                    </div>&ndash;&gt;-->
<!--              &lt;!&ndash;                    <div v-else>&ndash;&gt;-->
<!--              &lt;!&ndash;                      <p>Null</p>&ndash;&gt;-->
<!--              &lt;!&ndash;                    </div>&ndash;&gt;-->
<!--              &lt;!&ndash;                  </div>&ndash;&gt;-->
<!--              &lt;!&ndash;                  <div>&ndash;&gt;-->
<!--              &lt;!&ndash;                    <p>Adult:</p>&ndash;&gt;-->
<!--              &lt;!&ndash;                    <div v-if="item.adultUrl">&ndash;&gt;-->
<!--              &lt;!&ndash;                      <img :src="item.adultUrl" />&ndash;&gt;-->
<!--              &lt;!&ndash;                    </div>&ndash;&gt;-->
<!--              &lt;!&ndash;                    <div v-else>&ndash;&gt;-->
<!--              &lt;!&ndash;                      <p>Null</p>&ndash;&gt;-->
<!--              &lt;!&ndash;                    </div>&ndash;&gt;-->
<!--              &lt;!&ndash;                  </div>&ndash;&gt;-->
<!--              &lt;!&ndash;                </div>&ndash;&gt;-->
<!--              &lt;!&ndash;              </div>&ndash;&gt;-->

<!--              &lt;!&ndash;              <ul>&ndash;&gt;-->
<!--              &lt;!&ndash;                <li v-for="gene in convertId(item.geneIdSets)" :key="gene.id">&ndash;&gt;-->
<!--              &lt;!&ndash;                  {{ gene.trait }} - {{ gene.alleles }} - {{ gene.description }}&ndash;&gt;-->
<!--              &lt;!&ndash;                </li>&ndash;&gt;-->
<!--              &lt;!&ndash;              </ul>&ndash;&gt;-->
<!--            </li>-->
<!--          </ul>-->
<!--        </div>-->
<!--      </div>-->
    </div>
  </div>

  <!--  <div v-if="store.speciesGenes">-->
  <!--    <div v-for="(item, index) in store.speciesGenes" :key="index">-->
  <!--    {{ item.id }}-->
  <!--    </div>-->
  <!--  </div>-->

  <!--  <form @submit.prevent="store.submitImage()">-->
  <!--    <input-->
  <!--      type="text"-->
  <!--      v-model="store.image.speciesName"-->
  <!--      placeholder="Species Name"-->
  <!--      class="px-2 w-2/3 mb-2"-->
  <!--    />-->
  <!--    <input type="text" v-model="store.image.sex" placeholder="Sex" class="px-2 w-2/3 mb-2" />-->
  <!--    <input type="text" v-model="store.image.age" placeholder="Age" class="px-2 w-2/3 mb-2" />-->
  <!--    <div class="flex flex-wrap">-->
  <!--      <div v-for="(item, index) in store.image.images" :key="index">-->
  <!--        <img :src="item" alt="" class="mb-2" />-->
  <!--      </div>-->
  <!--    </div>-->
  <!--    &lt;!&ndash;    <img v-show="store.image.image" :src="store.image.image" alt="" class="mb-2" />&ndash;&gt;-->
  <!--    <input type="file" @change="handleImageSelected" accept="image/*" multiple />-->
  <!--    <button class="border">Submit</button>-->
  <!--  </form>-->

  <!--  <div v-if="store.portraits">-->
  <!--    <div v-for="(item, index) in store.portraits.portraits" :key="index" class="border-2 mb-2">-->
  <!--      <div>-->
  <!--        <p>{{ item.sex }}</p>-->
  <!--        <p>Juvenile:</p>-->
  <!--        <div v-if="item.juvenileUrl">-->
  <!--          <img :src="item.juvenileUrl" />-->
  <!--        </div>-->
  <!--        <div v-else>-->
  <!--          <p>Null</p>-->
  <!--        </div>-->
  <!--        <p>Adult:</p>-->
  <!--        <div v-if="item.adultUrl">-->
  <!--          <img :src="item.adultUrl" />-->
  <!--        </div>-->
  <!--        <div v-else>-->
  <!--          <p>Null</p>-->
  <!--        </div>-->
  <!--      </div>-->
  <!--      <ul>-->
  <!--        <li v-for="gene in convertId(item.geneIds)" :key="gene.id">-->
  <!--          {{ gene.trait }} - {{ gene.alleles }} - {{ gene.description }}-->
  <!--        </li>-->
  <!--      </ul>-->
  <!--    </div>-->
  <!--  </div>-->
</template>

<style scoped>
.checkbox-list {
  list-style-type: none;
  padding: 0;
}

.checkbox-list li {
  display: flex;
  align-items: center;
  padding: 10px;
  cursor: pointer;
  user-select: none;
}

.checkbox-list li:hover {
  background-color: #f0f0f0;
}

.checkbox {
  width: 20px;
  height: 20px;
  border: 2px solid #333;
  border-radius: 4px;
  margin-right: 10px;
  position: relative;
}

.checkbox-list li.selected .checkbox::after {
  content: '\2713';
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-size: 16px;
  color: #333;
}
</style>