import { ref } from 'vue';
import http from '../common/http-common';
import type { Product } from '../models/Product';

export default function useProducts() {
    const products = ref<Product[]>([]);
    const errors = ref([]);
    const getProducts = async (skip: number = 0) => {
        try {
            skip = skip === 0 ? 0 : --skip;
            let response = await http.get(`products/?take=10&skip=${skip}`)
        
            products.value = response.data
        } catch (error: any) {
            debugger;
            console.log(error.message);
        }
    }

    const addProduct = async (product: Product) => {
        errors.value = [];
        try {
            await http.post('products', product);
        } catch (e: any) {
            if (e.response.status === 400) { //Bad request, for validation in .net core
                debugger;
                // for (const key in e.response.data.errors) {
                //     errors.value.push(e.response.data.errors[key][0]) ;
                // }
            }
        }
    }

    const editProduct = async (product: Product) => {
        errors.value = [];
        try {
            await http.put('products', product);
        } catch (e: any) {
            if (e.response.status === 400) { //Bad request, for validation in .net core
                debugger;
                // for (const key in e.response.data.errors) {
                //     errors.value.push(e.response.data.errors[key][0]) ;
                // }
            }
        }
    }

    const deleteProduct = async (productId: number) => {
        errors.value = [];
        try {
            debugger;

            await http.delete(`products/?productId=${productId}`);

            // return true;
            // await router.push({ name: 'movies.index' })
        } catch (e: any) {
            if (e.response.status === 400) { //Bad request, for validation in .net core
                debugger;
                // for (const key in e.response.data.errors) {
                //     errors.value.push(e.response.data.errors[key][0]) ;
                // }
            }
        }
    }

    return {
        errors,
        products,
        getProducts,
        addProduct,
        editProduct,
        deleteProduct,
    }
}