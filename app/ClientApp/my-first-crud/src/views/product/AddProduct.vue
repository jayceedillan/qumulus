
<template>
  <v-row justify="end" class="mr-4">
    <v-dialog v-model="dialog" persistent width="800">
      <template v-slot:activator="{ props }">
        <v-btn color="white" elevation="" v-bind="props"> Add Product </v-btn>
      </template>
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
                    v-model="formProduct.code"
                  ></v-text-field>
                  <span class="required" color="red" v-if="v$.code.$error">Code is required</span>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    label="Name*"
                    variant="outlined"
                    clearable
                    v-model="formProduct.name"
                  >
                  </v-text-field>
                  <span class="required" color="red" v-if="v$.name.$error">Name is required</span>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    label="Description"
                    variant="outlined"
                    clearable
                    v-model="formProduct.description"
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-select
                    :items="categories"
                    label="Category*"
                    item-title="name"
                    item-value="id"
                    v-model="formProduct.productCategory"
                  ></v-select>
                    
                  <span class="required" color="red" v-if="v$.productCategory.$error"
                    >Product Category is required</span
                  >
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    label="SKU"
                    variant="outlined"
                    clearable
                    v-model="formProduct.sku"
                  ></v-text-field>
                  <span class="required" color="red" v-if="v$.sku.$error">SKU is required</span>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    label="Price*"
                    type="number"
                    variant="outlined"
                    clearable
                    v-model="formProduct.price"
                  ></v-text-field>
                  <span class="required" color="red" v-if="v$.price.$error">Price is required</span>
                </v-col>
              </v-row>
            </v-container>
            <small>*indicates required field</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue-darken-1" variant="text" @click="dialog = false"> Close </v-btn>
            <v-btn color="blue-darken-1" type="submit" variant="text"> Save </v-btn>
          </v-card-actions>
        </form>
      </v-card>
    </v-dialog>
  </v-row>
</template>
  <script setup lang="ts">
import { computed, reactive, ref } from 'vue'
import useProducts from '../../composables/products'
import useVuelidate from '@vuelidate/core'
import { required } from '@vuelidate/validators'
import { categoriesItems} from '../../common/categories'
// export default {
//   setup() {

    const emit = defineEmits(['addProduct'])

    let dialog = ref<boolean>(false)

    const categories = ref(categoriesItems)
    const initState = {
      productId: 0,
      name: '',
      productCategory: 0,
      description: '',
      price: 0,
      sku: '',
      code: ''
    }
    let formProduct = reactive({ ...initState })

    const rules = computed(() => {
      return {
        name: { required },
        productCategory: { required },
        description: '',
        price: { required },
        sku: { required },
        code: { required }
      }
    })

    const v$ = useVuelidate(rules, formProduct)

    const { errors, addProduct } = useProducts()

    const saveProduct = async () => {
      debugger
      const result = await v$.value.$validate()

      if (result) {
        await addProduct({ ...formProduct })
        v$.value.$reset()
        dialog.value = errors.value.length !== 0
        Object.assign(formProduct, initState)
        emit('addProduct', errors.value.length == 0)
      }
    }

    // return {
    //   dialog,
    //   formProduct,
    //   addProduct,
    //   v$,
    //   saveProduct,
    //   categories
    // }
//   }
// }
</script>
<style scoped>
.required {
  color: red;
  margin-top: -20px;
  position: absolute;
  font-size: 12px;
}
</style>