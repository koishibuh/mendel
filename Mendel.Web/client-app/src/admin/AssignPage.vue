<script lang="ts" setup>
import { useImageGeneStore } from '@/admin/image-gene.store';
import { useSpeciesStore } from '@/admin/species.store';
import { type IOrphanedImage, useOrphanStore } from '@/admin/orphan.store';
import type { IGenotype } from '@/checklist/checklist.store';
import DropdownMenu from '@/components/dropdown-menu/DropdownMenu.vue';

const imageGeneStore = useImageGeneStore();
const speciesStore = useSpeciesStore();
const orphanStore = useOrphanStore();

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

const getOrphanedImages = async () => {
  const speciesId = imageGeneStore.selectedSpecies?.id;
  if (speciesId) {
    await orphanStore.getOrphanedImages(speciesId);
  }
};
</script>
<template>
  <div class="p-2 flex flex-col">
    <h2>Assign Images to Genes</h2>
    <div class="flex gap-2 mb-2">
      <DropdownMenu
        :menu="speciesStore.options"
        @option-selected="imageGeneStore.updateSelectedSpecies"
      />
      {{ imageGeneStore.selectedSpecies?.id }}
      <input type="button" value="Get Orphaned Images" @click="getOrphanedImages" />
      <input type="button" value="Get Genes" @click="imageGeneStore.getImageGenes()" />
    </div>

    <div class="grid grid-cols-2 gap-2">
      <div v-if="orphanStore.orphanedImages">
        <ul class="grid grid-cols-2 gap-2 checkbox-list">
          <li
            v-for="(item, index) in orphanStore.orphanedImages"
            :key="index"
            class="border-2 border-black"
            @click="handleOrphanSelected(item)"
            :class="{ selected: orphanStore.selectedOrphans.some((x) => x === item.id) }"
          >
            <span class="checkbox"></span>
            <div>
              <div>
                {{ item.id }}
                {{ item.sex }}
                {{ item.age }}
              </div>
              <div>
                <img :src="item.url" />
              </div>
            </div>
          </li>
        </ul>
      </div>

      <div>
        <div v-if="imageGeneStore.newSelectedSpeciesGenes">
          <!--              {{ imageGeneStore.selectedSpeciesGenes.genes.find((x) => x.trait === 'Unknown').id }}-->
          <ul class="">
            <li
              v-for="(image, index) in imageGeneStore.newSelectedSpeciesGenes.images"
              :key="index"
              class="border-black border-2 mb-2"
            >
              <div>{{ image.sex }}</div>
              <div class="grid grid-cols-3">
                <div class="bg-r p-2">
                  <p class="bg-yellow-300">Juvenile</p>
                  <p>Id: {{ image.url }}</p>
                </div>
              </div>
<!--              <div class="bg-g p-2">-->
<!--                <p>Adult</p>-->
<!--                <p>Id: {{ image.adultImageId }}</p>-->
<!--                <div v-if="image.adultUrl">-->
<!--                  <img :src="image.adultUrl" />-->
<!--                </div>-->
<!--                <div v-else>-->
<!--                  <p>Image Not Found</p>-->
<!--                </div>-->
<!--                <button>Delete</button>-->
<!--              </div>-->
<!--              <div class="bg-b p-2">-->
<!--                <p>Genes</p>-->
<!--                <div v-for="(geneSet, index) in image.geneIdSets" :key="index">-->
<!--                  <div v-for="gene in convertId(geneSet)" :key="gene.id">-->
<!--                    {{ gene.id }}-->
<!--                    {{ gene.trait }}-->
<!--                    {{ gene.alleles }}-->
<!--                    {{ gene.description }}-->
<!--                  </div>-->
<!--                </div>-->
<!--              </div>-->
<!--        </div>-->
        </li>
        </ul>
      </div>
    </div>
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