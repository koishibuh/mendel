<script setup lang="ts">
import { ref } from 'vue';

const props = defineProps({
  option1Name: {
    type: String,
    default: 'Option 1'
  },
  option2Name: {
    type: String,
    default: 'Option 2'
  },
  option3Name: {
    type: String,
    default: 'Option 3'
  },
  stateValue: {
    type: Number
  }
});

const emit = defineEmits<{
  updateState: [state: string];
}>();

const buttonClicked = (buttonvalue: string) => {
  selectedValue.value = buttonvalue;
  emit('updateState', buttonvalue);
};

const selectedValue = ref<string>(props.option3Name);
</script>

<template>
  <div class="grid grid-flow-col justify-stretch">
    <button
      class="toggle-button"
      @click="buttonClicked(option1Name)"
      :disabled="selectedValue === option1Name"
    >
      {{ option1Name }}
    </button>
    <button
      class="toggle-button"
      @click="buttonClicked(option2Name)"
      :disabled="selectedValue === option2Name"
    >
      {{ option2Name }}
    </button>
    <button
      class="toggle-button"
      @click="buttonClicked(option3Name)"
      :disabled="selectedValue === option3Name"
    >
      {{ option3Name }}
    </button>
  </div>
</template>