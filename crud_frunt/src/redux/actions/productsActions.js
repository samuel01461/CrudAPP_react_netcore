import { GetMyProducts as ApiGetMyProducts, GetAllProducts as ApiGetAllProducts, CreateProduct as ApiCreateProduct, EditProduct as ApiEditProduct, DeleteProduct as ApiDeleteProduct } from "../../services/api.js";
import { RefreshToken as ApiRefreshToken } from "../../services/api.js";

export const GET_PRODUCTS = "GET_PRODUCTS";
export const GET_MYPRODUCTS = "GET_MYPRODUCTS";
export const NEW_PRODUCT = "CREATE_PRODUCT";
export const EDIT_PRODUCT = "EDIT_PRODUCT";
export const DELETE_PRODUCT = "DELETE_PRODUCT";

export const GetAllProducts = () => async dispatch => {
    try {
        const response = await ApiGetAllProducts();
        var products = response.data.products;

        const newState = {
            products: products
        };
        
        dispatch({ type: GET_PRODUCTS, payload: newState });
    } catch (e) {
        console.error(e);
    }
};

export const GetMyProducts = () => async dispatch => {
    try {
        const response = await ApiGetMyProducts();
        var products = response.data.products;

        const newState = {
            myProducts: products
        };
        
        dispatch({ type: GET_MYPRODUCTS, payload: newState });
    } catch (e) {
        const refresh_response = await ApiRefreshToken();
        console.log(refresh_response);
    }
};

export const CreateProduct = (product) => async dispatch => {
    try {
        const response = await ApiCreateProduct(product);
        dispatch({ type: NEW_PRODUCT, payload: response.data.product })
    } catch (e) {
        console.error(e);
    }
};

export const EditProduct = (product, new_myProducts) => async dispatch => {
    try {
        ApiEditProduct(product);
        dispatch({ type: EDIT_PRODUCT, payload: new_myProducts })
    } catch (e) {
        console.error(e);
     }
}

export const DeleteProduct = (product, new_myProducts) => async dispatch => {
    try {
        await ApiDeleteProduct(product);
        dispatch({ type: DELETE_PRODUCT, payload: new_myProducts });
    } catch (e) {
        console.error(e);
    }
};