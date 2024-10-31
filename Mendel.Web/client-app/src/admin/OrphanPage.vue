<script lang="ts" setup>
import { type ICloudinary, type IOrphanedImage, useOrphanStore } from '@/admin/orphan.store.js';
import { useSpeciesStore } from '@/admin/species.store';
import { useImageGeneStore } from '@/admin/image-gene.store';
import { onMounted } from 'vue';
import DropdownMenu from '@/components/dropdown-menu/DropdownMenu.vue';
import type { IGenotype } from '@/checklist/checklist.store';

const orphanStore = useOrphanStore();
const speciesStore = useSpeciesStore();
const imageGeneStore = useImageGeneStore();

// onMounted(async () => {
//   await orphanStore.getOrphanedImages();
// });

const convertId = (geneId: number[]): IGenotype[] => {
  return imageGeneStore.selectedSpeciesGenes.genes.filter((x) => geneId.includes(x.id));
};

const handleOrphanSelected = (image: IOrphanedImage) => {
  const index = orphanStore.selectedOrphans.findIndex((x) => x === image.id);
  if (index === -1) {
    orphanStore.selectedOrphans.push(image.id);
  } else {
    orphanStore.selectedOrphans.splice(index, 1);
  }
};

const handleCloudinarySelected = (image: ICloudinary) => {
  const index = orphanStore.selectedCloudinaryImages.images.findIndex((x) => x.publicId === image.publicId);
  if (index === -1) {
    orphanStore.selectedCloudinaryImages.images.push(image);
  } else {
    orphanStore.selectedCloudinaryImages.images.splice(index, 1);
  }
};

const assignUnknownGeneToOrphans = async () => {
  if (imageGeneStore.selectedSpeciesGenes) {
    const unknownId = imageGeneStore.selectedSpeciesGenes.genes
      .find((x) => x.trait === 'Unknown')
      ?.id ?? -1; // Use a default value if id is undefined

    if (unknownId !== -1) {
      await orphanStore.assignUnknownGeneToOrphans(unknownId);
    } else {
      console.error('Unknown gene not found');
    }
  }
};
</script>

<template>
  <div class="p-2">
    <h2>Orphaned Images</h2>

    <div>
<!--      <DropdownMenu-->
<!--        :menu="speciesStore.options"-->
<!--        @option-selected="imageGeneStore.updateSelectedSpecies"-->
<!--      />-->

      <label for="name">Name</label>
      <input
        id="name"
        v-model="orphanStore.cloudinarySearch"
        type="text"
        placeholder=""
      />
      <button @click="orphanStore.getCloudinaryImages()">Get Cloudinary Images</button>

      <div>

      <label for="speciesId">Species Id</label>
      <input
        id="speciesId"
        v-model="orphanStore.selectedCloudinaryImages.speciesId"
        type="text"
        placeholder=""
      />

      <label for="age">Age</label>
      <input
        id="age"
        v-model="orphanStore.selectedCloudinaryImages.age"
        type="text"
        placeholder=""
      />

      <label for="sex">Sex</label>
      <input
        id="sex"
        v-model="orphanStore.selectedCloudinaryImages.sex"
        type="text"
        placeholder=""
      />
      <button @click="orphanStore.saveCloudinaryImages()">Save Images To Unknown</button>
      </div>

    </div>



<!--    <div v-if="imageGeneStore.selectedSpeciesGenes">-->


<!--      <div v-for="(item, index) in imageGeneStore.imagesWithUnknown" :key="index" class="bg-r">-->
<!--        {{ item }}-->
<!--      </div>-->

<!--      <div v-for="(item1, index) in imageGeneStore.imagesWithoutUnknown" :key="index" class="bg-b">-->
<!--        {{ item1 }}-->

<!--      </div>-->

<!--    </div>-->

    Selected Cloudinary count: {{ orphanStore.selectedCloudinaryImages.images.length }}
    <div class="flex gap-2 mb-2">
      <button>Delete</button>
      <button>Add Gene</button>
      <button @click="assignUnknownGeneToOrphans">Add Unknown Gene</button>
    </div>

    <div class="grid grid-cols-2 gap-2">
      <div v-if="orphanStore.cloudinaryResults">
        <ul class="grid grid-cols-2 gap-2 checkbox-list">
          <li
            v-for="item in orphanStore.cloudinaryResults"
            :key="item.publicId"
            class="border-2 border-black"
            @click="handleCloudinarySelected(item)"
            :class="{ selected: orphanStore.selectedCloudinaryImages.images.some((x) => x.publicId === item.publicId) }"
          >
            <span class="checkbox"></span>
            <img :src="item.url" />
          </li>
        </ul>
      </div>


      <input type="button" value="Submit" @click="imageGeneStore.getImageGenes()" />

<!--    Selected Orphan count: {{ orphanStore.selectedOrphans.length }}-->
<!--    {{ orphanStore.selectedOrphans }}-->
<!--    <div class="flex gap-2 mb-2">-->
<!--      <button>Delete</button>-->
<!--      <button>Add Gene</button>-->
<!--      <button @click="assignUnknownGeneToOrphans">Add Unknown Gene</button>-->
<!--    </div>-->
<!--    <div class="grid grid-cols-2 gap-2">-->
<!--      <div v-if="orphanStore.orphanedImages">-->
<!--        <ul class="grid grid-cols-2 gap-2 checkbox-list">-->
<!--          <li-->
<!--            v-for="(item, index) in orphanStore.orphanedImages"-->
<!--            :key="index"-->
<!--            class="border-2 border-black"-->
<!--            @click="handleOrphanSelected(item)"-->
<!--            :class="{ selected: orphanStore.selectedOrphans.some((x) => x === item.id) }"-->
<!--          >-->
<!--            <span class="checkbox"></span>-->
<!--            {{ item.id }}-->
<!--            <img :src="item.url" />-->
<!--            {{ item.sex }}-->
<!--            {{ item.age }}-->
<!--          </li>-->
<!--        </ul>-->
<!--      </div>-->


<!--      <input type="button" value="Submit" @click="imageGeneStore.getImageGenes()" />-->




    <!--      <div>-->
    <!--        Genes, Select a Species-->
    <!--        <div class="flex gap-2">-->
    <!--          <DropdownMenu-->
    <!--            :menu="speciesStore.options"-->
    <!--            @option-selected="imageGeneStore.updateSelectedSpecies"-->
    <!--          />-->
    <!--          <input type="button" value="Submit" @click="imageGeneStore.getImageGenes()" />-->
    <!--        </div>-->
    <!--        <div v-if="imageGeneStore.selectedSpeciesGenes">-->
    <!--          {{ imageGeneStore.selectedSpeciesGenes.genes.find((x) => x.trait === 'Unknown').id }}-->
    <!--          <ul class="">-->
    <!--            <li-->
    <!--              v-for="(image, index) in imageGeneStore.selectedSpeciesGenes.images"-->
    <!--              :key="index"-->
    <!--              class="border-black border-2 mb-2"-->
    <!--            >-->
    <!--              <div>{{ image.sex }}</div>-->
    <!--              <div class="grid grid-cols-3">-->
    <!--                <div class="bg-r p-2">-->
    <!--                  <p class="bg-yellow-300">Juvenile</p>-->
    <!--                  <p>Id: {{ image.juvenileImageId }}</p>-->
    <!--                  <div v-if="image.juvenileUrl" class="bg-b grow items-center justify-center">-->
    <!--                    <img :src="image.juvenileUrl" class="max-w-full max-h-full object-contain" />-->
    <!--                  </div>-->
    <!--                  <div v-else class="text-center grow">-->
    <!--                    <p>Image Not Found</p>-->
    <!--                  </div>-->
    <!--                  <div class="bg-yellow-300">-->
    <!--                    <button-->
    <!--                      class=""-->
    <!--                      @click="-->
    <!--                        imageGeneStore.removeImageFromGene(image.juvenileImageId, image.geneIdSets)-->
    <!--                      "-->
    <!--                    >-->
    <!--                      Delete-->
    <!--                    </button>-->
    <!--                  </div>-->
    <!--                </div>-->
    <!--                <div class="bg-g p-2">-->
    <!--                  <p>Adult</p>-->
    <!--                  <p>Id: {{ image.AdultImageId }}</p>-->
    <!--                  <div v-if="image.adultUrl">-->
    <!--                    <img :src="image.adultUrl" />-->
    <!--                  </div>-->
    <!--                  <div v-else>-->
    <!--                    <p>Image Not Found</p>-->
    <!--                  </div>-->
    <!--                  <button>Delete</button>-->
    <!--                </div>-->
    <!--                <div class="bg-b p-2">-->
    <!--                  <p>Genes</p>-->
    <!--                  <div v-for="(geneSet, index) in image.geneIdSets" :key="index">-->
    <!--                    <div v-for="gene in convertId(geneSet)" :key="gene.id">-->
    <!--                      {{ gene.id }}-->
    <!--                      {{ gene.trait }}-->
    <!--                      {{ gene.alleles }}-->
    <!--                      {{ gene.description }}-->
    <!--                    </div>-->
    <!--                  </div>-->
    <!--                </div>-->
    <!--              </div>-->
    <!--            </li>-->
    <!--          </ul>-->
    <!--        </div>-->
    <!--      </div>-->
  </div>
  </div>
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