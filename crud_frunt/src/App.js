import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import Products from "./components/Products";
import Users from "./components/Users";
import NBar from "./components/NBar";
import { useSelector } from "react-redux";
import MyProducts from "./components/MyProducts";

function App() {
  const isAuthenticated = useSelector(state => state.auth.isAuthenticated);

  return (
    <div className="App">
      { isAuthenticated && <NBar />}
        <Routes>
        <Route path="/" element={<Login />}></Route>
        <Route path="/login" element={<Login />}></Route>
        <Route path="/products" element={<Products />}></Route>
        <Route path="/myProducts" element={<MyProducts />}></Route>
        <Route path="/users" element={<Users />}></Route>
        </Routes>
    </div>
  );
}

export default App;
