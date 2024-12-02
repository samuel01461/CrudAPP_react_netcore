import { LOGIN_SUCCESS, LOGIN_FAILED, LOGOUT, REFRESH_TOKEN } from "../actions/authActions";

const initialState = {
    isAdmin: false,
    userId: null,
    access_token: null,
    refresh_token: null,
    isAuthenticated: false,
    loading: false,
    loginError: false,
    errorMessage: null
};

const authReducer = (state = initialState, action) => {
    var new_state = action.payload;

    switch(action.type) {
        case LOGIN_SUCCESS:
            return { ...state, userId: new_state.userId, access_token: new_state.access_token, refresh_token: new_state.refresh_token, isAuthenticated: true, loginError: false, errorMessage: null, loading: false, isAdmin: new_state.isAdmin };
        case LOGIN_FAILED:
            return { ...state, userId: null, access_token: null, refresh_token: null, isAuthenticated: false, loginError: true, errorMessage: new_state.errorMessage, loading: false };
        case LOGOUT:
            return { ...state, userId: null, access_token: null, refresh_token: null, isAuthenticated: false, loginError: false, errorMessage: null, loading: false };
        case REFRESH_TOKEN:
            return { ...state, userId: new_state.userId, access_token: new_state.access_token, refresh_token: new_state.refresh_token, isAuthenticated: true, loginError: false, errorMessage: null, loading: false };
        default: 
            return state;
    }
};

export default authReducer;