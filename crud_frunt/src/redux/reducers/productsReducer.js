import { GET_PRODUCTS, GET_MYPRODUCTS, NEW_PRODUCT, EDIT_PRODUCT, DELETE_PRODUCT }  from "../actions/productsActions";

const initialState = {
    products: [],
    myProducts: [],
    currentProduct: null
}

const productsReducer = (state = initialState, action) => {
    switch(action.type) {
        case GET_PRODUCTS:
            return { ...state, products: action.payload.products };
        case GET_MYPRODUCTS:
            return { ...state, myProducts: action.payload.myProducts };
        case NEW_PRODUCT:
            return { ...state, myProducts: [ ...state.myProducts, action.payload ] };
        case EDIT_PRODUCT:
            return { ...state, myProducts: action.payload };
        case DELETE_PRODUCT:
            return { ...state, myProducts: action.payload };
        default:
            return state;
    }
};

export default productsReducer;