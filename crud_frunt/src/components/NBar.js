
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { Link } from "react-router-dom";
import { Logout } from '../redux/actions/authActions';
import { useDispatch, useSelector } from 'react-redux';

function NBar() {
    const isAdmin = useSelector(state => state.auth.isAdmin);
    const dispatch = useDispatch();
    const handleLogout = () => {
        dispatch(Logout());
    };

    return (
        <>
    <Navbar bg="light" expand="lg" >
        <Navbar.Brand className="ms-3"> Productos CRUD</Navbar.Brand>
        <Navbar.Toggle aria-controls='responsive-navbar-nav'/>
    <Navbar.Collapse id="responsive-navbar-nav">
    <Nav className="ms-auto">
        <Nav.Link as={Link} to="/">Inicio</Nav.Link>
        <Nav.Link as={Link} to="/myProducts">Mis productos</Nav.Link>
        <Nav.Link as={Link} to="/products">Todos los productos</Nav.Link>
        { isAdmin && <Nav.Link as={Link} to="/users">Usuarios</Nav.Link> }
    </Nav>
    <Nav className="ms-auto">
        <Nav.Link onClick={handleLogout} >Salir</Nav.Link>
    </Nav>
    </Navbar.Collapse>
    </Navbar>
    </>
    );
}

export default NBar;
