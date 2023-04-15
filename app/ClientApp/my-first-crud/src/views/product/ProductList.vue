<template>
  <v-container>
    <v-card max-width="1200" class="mx-auto">
      <v-toolbar color="deep-purple accent-4">
        <v-toolbar-title> Products </v-toolbar-title>
        <v-spacer></v-spacer>
        <AddProduct @addProduct="addProduct"></AddProduct>
      </v-toolbar>
      <v-table height="600px">
        <thead>
          <tr>
            <th class="text-left">Name</th>
            <th class="text-left">Description</th>
            <th class="text-left">Price</th>
            <th class="text-left">Action</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in products" :key="item.code">
            <td>{{ item.name }}</td>
            <td>{{ item.description }}</td>
            <td>{{ item.price }}</td>
            <td>
              <v-row>
                <v-col cols="12">
                  <v-btn
                    class="ma-2"
                    variant="text"
                    icon="mdi-pencil"
                    color="blue-lighten-2"
                    @click="editProduct(item)"
                  ></v-btn>
                  <v-btn
                    class="ma-2"
                    variant="text"
                    icon="mdi-delete"
                    color="red-lighten-2"
                    @click="deleteByProductId(item.productId)"
                  ></v-btn>
                </v-col>
              </v-row>
            </td>
          </tr>
        </tbody>
      </v-table>
      {{ page }}
      <v-pagination v-model="page" :length="6"></v-pagination>
    </v-card>

    <EditProduct
      :productTest="product"
      :dialogs="dialog"
      v-if="dialog"
      @emitEditProduct="emitEditProduct"
    ></EditProduct>

    <ConfirmDialog
      :name="xxx"
      :dialogs="showConfirm"
      @closeModal="closeModal"
      v-if="showConfirm"
    ></ConfirmDialog>

    <v-snackbar :timeout="2000" color="primary" variant="tonal" :top="top" v-model="snackbar">
      {{ message }}
      <template v-slot:actions>
        <v-btn color="pink" variant="text" @click="snackbar = false"> Close </v-btn>
      </template>
    </v-snackbar>
  </v-container>
</template>

<script lang="ts">
import { onMounted, ref, watch } from 'vue'
import useProducts from '../../composables/products'
import AddProduct from '../product/AddProduct.vue'
import EditProduct from '../product/EditProduct.vue'
import ConfirmDialog from '../../components/ConfirmDailog.vue'
import type { Product } from '../../models/Product'
import { MESSAGE_ADDED, MESSAGE_DELETED, MESSAGE_UPDATED } from '../../common/constant'
export default {
  components: {
    AddProduct,
    EditProduct,
    ConfirmDialog
  },
  setup() {
    const product = ref<Product>([])
    const snackbar = ref(false)
    const dialog = ref<boolean>(false)
    const page = ref(1)
    const showConfirm = ref<boolean>(false)
    let message = ref('')
    const { products, getProducts, deleteProduct } = useProducts()

    onMounted(getProducts)

    const addProduct = (isSuccess: boolean) => {
      if (isSuccess) {
        showNotification(MESSAGE_ADDED)
      }
    }
    const editProduct = (productItem: Product) => {
      product.value = productItem
      dialog.value = true
    }

    const emitEditProduct = async (isSuccess: boolean) => {
      if (isSuccess) {
        showNotification(MESSAGE_UPDATED)
        await getProducts()
      }
      dialog.value = false
    }

    const deleteByProductId = (productId: number) => {
      product.value.productId = productId
      showConfirm.value = true
    }

    watch(page, async (currentValue) => {
      await getProducts(currentValue)
    })

    const closeModal = async (isDelete: boolean) => {
      if (isDelete) {
        await deleteProduct(product.value.productId)
        await getProducts()
        showNotification(MESSAGE_DELETED)
      }
      showConfirm.value = false
    }

    const showNotification = (mesg: string) => {
      snackbar.value = true
      message.value = mesg
    }
    return {
      products,
      dialog,
      product,
      page,
      deleteByProductId,
      editProduct,
      deleteProduct,
      addProduct,
      closeModal,
      emitEditProduct,
      snackbar,
      showConfirm,
      message
    }
  }
}
</script>
