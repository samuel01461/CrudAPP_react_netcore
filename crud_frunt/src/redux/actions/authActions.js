import { jwtDecode } from "jwt-decode";
import { Login as ApiLogin } from "../../services/api";

export const LOGIN_SUCCESS = "LOGIN_SUCCESS";
export const LOGIN_FAILED = "LOGIN_FAILED";
export const LOGOUT = "LOGOUT";
export const REFRESH_TOKEN = "REFRESH_TOKEN";

export const Login = (credentials) => async dispatch => {
    try {
        const response = await ApiLogin(credentials);
        const jwt_decode = jwtDecode(response.data.accessToken);
        const userId_key = Object.keys(jwt_decode)[2];
        const userId = jwt_decode[userId_key];
        
        const userRole = jwt_decode["role"];
        var isAdmin = false;
        
        if (userRole === "1") {
            isAdmin = true;
        } 

        const newState = {
            userId: userId,
            access_token: response.data.accessToken,
            refresh_token: response.data.refreshToken,
            isAdmin: isAdmin
        };

        localStorage.setItem("access_token", newState.access_token);
        localStorage.setItem("refresh_token", newState.refresh_token);
        localStorage.setItem("userId", newState.userId);
        dispatch({ type: LOGIN_SUCCESS, payload: newState });
    } catch (e) {
        if (e.status === 401) {
            const newState = {
                errorMessage: "Error de credenciales"
            }

            dispatch({ type: LOGIN_FAILED, payload: newState });
        }
    }
}

export const Logout = () => async dispatch => {
    dispatch({ type: LOGOUT, payload: null });
};