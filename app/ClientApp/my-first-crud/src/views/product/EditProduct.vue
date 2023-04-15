
<template>
  <v-row>
    <v-dialog v-model="dialog" persistent width="800">
      <v-card>
        <form @submit.prevent="saveProduct">
          <v-card-title>
            <span class="text-h5">Product Information</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12">
                  <v-text-field
                    label="Code*"
                    variant="outlined"
                    clearable
                    required
                    v-model="product.code"
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    label="Name*"
                    variant="outlined"
                    clearable
                    required
                    v-model="product.name"
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    label="Description"
                    variant="outlined"
                    clearable
                    required
                    v-model="product.description"
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-select
                    :items="categories"
                    label="Category*"
                    item-title="name"
                    item-value="id"
                    v-model="product.productCategory"
                  ></v-select>
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    label="SKU"
                    variant="outlined"
                    clearable
                    required
                    v-model="product.sku"
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    label="Price*"
                    type="number"
                    variant="outlined"
                    clearable
                    required
                    v-model="product.price"
                  ></v-text-field>
                </v-col>
              </v-row>
            </v-container>
            <small>*indicates required field</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue-darken-1" variant="text" @click="closeModal()"> Close </v-btn>
            <v-btn color="blue-darken-1" type="submit" variant="text"> Save </v-btn>
          </v-card-actions>
        </form>
      </v-card>
    </v-dialog>
  </v-row>
</template>
  <script setup lang="ts">
import { reactive, ref } from 'vue'
import { categoriesItems } from '../../common/categories'
import useProducts from '../../composables/products'
import type { Product } from '../../models/Product'

const props = defineProps<{
  productTest: Product
  dialogs: boolean
}>()

const emit = defineEmits(['emitEditProduct'])
const dialog = ref<boolean>(false)

dialog.value = props.dialogs

const categories = ref(categoriesItems)

const id = props.productTest.productCategory as any
let category = categories.value.find((c) => c.id === id)

if (!category) {
  category = { id: 1, name: '' }
}
const product = reactive({
  productId: props.productTest.productId,
  name: props.productTest.name,
  productCategory: props.productTest.productCategory,
  description: props.productTest.description,
  price: props.productTest.price,
  sku: props.productTest.sku,
  code: props.productTest.code,
  productCategoryId: 0,
})

const { errors, editProduct } = useProducts()

const saveProduct = async () => {
  await editProduct({ ...product })
  dialog.value = errors.value.length !== 0
  emit('emitEditProduct', errors.value.length == 0)
}

const closeModal = async () => {

  dialog.value = false;
  emit('emitEditProduct', false)
}
</script>
