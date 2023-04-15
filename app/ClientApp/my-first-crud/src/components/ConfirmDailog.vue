

<template>
  <v-row justify="end" class="mr-4">
    <v-dialog v-model="dialog" persistent width="300">
      <v-card>
        <v-card-title>
          <span class="text-h6">Delete</span>
        </v-card-title>
        <v-card-text>
          <small> Are you sure you want to delete?</small>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue-darken-1" variant="text" @click="closeModal(false)"> No </v-btn>
          <v-btn color="blue-darken-1" variant="text" @click="closeModal(true)"> Yes </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>
   <script setup  lang="ts">
import { ref } from 'vue'

const props = defineProps<{
  dialogs: boolean
}>()
const emit = defineEmits(['closeModal'])

let dialog = ref<boolean>(false)
dialog.value = props.dialogs

const closeModal = async (isClose: boolean) => {
  emit('closeModal', isClose)
  dialog.value = false
}
</script>
    