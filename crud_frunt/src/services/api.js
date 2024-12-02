import axios from "axios";

const API_URL = "https://localhost:7147/api/";

const API_CLIENT = axios.create({
    baseURL: API_URL
});

API_CLIENT.interceptors.request.use(config => {
    const access_token = localStorage.getItem("access_token");
    if(access_token) {
        config.headers.Authorization = "Bearer " + access_token;
    }
    return config;
});

export const Login = async (credentials) => {
    return await API_CLIENT.post("Auth/Login", credentials);
}

export const RefreshToken = async () => {
    const refresh_token = localStorage.getItem("refresh_token");
    const userId = localStorage.getItem("userId");

    const data = {
        userId: userId,
        refresh_token: refresh_token
    }

    return await API_CLIENT.post("Auth/RefreshToken", data);
}

export const GetAllProducts = async () => {
    return await API_CLIENT.get("Products");
}

export const GetMyProducts = async () => {
    return await API_CLIENT.get("Products/GetMyProducts");
}

export const CreateProduct = async (product) => {
    return await API_CLIENT.post("Products", product);
}

export const EditProduct = async (product) => {
    return await API_CLIENT.put("Products", product);
}

export const DeleteProduct = async (productId) => {
    return await API_CLIENT.delete("Products/" + productId);
}