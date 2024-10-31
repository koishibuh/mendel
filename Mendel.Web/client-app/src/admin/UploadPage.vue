<script lang="ts" setup>
import { ref, watch } from 'vue';
import DropdownMenu from '@/components/dropdown-menu/DropdownMenu.vue';
import { useImageGeneStore } from '@/admin/image-gene.store';
import { useSpeciesStore } from '@/admin/species.store';
import { useUploadStore} from '@/admin/upload.store';

const speciesStore = useSpeciesStore();
const imageGeneStore = useImageGeneStore();
const uploadStore = useUploadStore();
const imageFiles = ref<File[]>([]);

function handleImageSelected(event: Event) {
  const target = event.target as HTMLInputElement;
  if (target.files) {
    imageFiles.value = Array.from(target.files);
  }
}

watch(imageFiles, (files) => {
  if (!files.length) return;
  files.forEach((file, index) => {
    if (!(file instanceof File)) return;

    const fileReader = new FileReader();
    fileReader.readAsDataURL(file);
    fileReader.addEventListener('load', () => {
      if (!Array.isArray(uploadStore.image.images)) {
        uploadStore.image.images = [];
      }
      uploadStore.image.images[index] = fileReader.result;
    });
  });
});
</script>

<template>
  <div class="p-2">
  <h2>
    Upload
  </h2>

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

      <form @submit.prevent="uploadStore.uploadImages()">
        <input
          type="text"
          v-model="uploadStore.image.speciesName"
          placeholder="Species Name"
          class="px-2 w-2/3 mb-2"
        />
        <input type="text" v-model="uploadStore.image.sex" placeholder="Sex" class="px-2 w-2/3 mb-2" />
        <input type="text" v-model="uploadStore.image.age" placeholder="Age" class="px-2 w-2/3 mb-2" />
        <div class="flex flex-wrap">
          <div v-for="(item, index) in uploadStore.image.images" :key="index">
            <img :src="item" alt="" class="mb-2" />
          </div>
        </div>
        <input type="file" @change="handleImageSelected" accept="image/*" multiple />
        <button class="border">Submit</button>
      </form>


  </div>
</template>