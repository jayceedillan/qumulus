export interface Product {
  productId: number
  name: string
  productCategory: productCategory
  description: string
  price: number
  sku: string
  code: string
}

enum productCategory {
  Clothing = 1,
  Footware = 2,
  Electronics = 3,
  Household = 4
}