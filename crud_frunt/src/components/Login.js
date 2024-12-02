import { useEffect, useState } from "react";
import styles from "../styles/Login.module.css";
import { Alert, Button, Container, Form } from "react-bootstrap";
import { Login as LoginAction } from "../redux/actions/authActions";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

function Login() {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const errorMessage = useSelector(state => state.auth.errorMessage);
    const loginError = useSelector(state => state.auth.loginError);
    const isAuthenticated = useSelector(state => state.auth.isAuthenticated);
    
    
    useEffect(() => {
        if (isAuthenticated) {
            navigate("/products");
        }
    }, [isAuthenticated, navigate]);

    const handleSubmit = (e) => {
        e.preventDefault();
        
        var credentials = {
            username: username,
            password: password
        }
        dispatch(LoginAction(credentials));
    };
    return (
        <>
        { !isAuthenticated ? (
        <Container align="center">
        <div className={styles.login}>
        <Form className='form mt-5 w-50' onSubmit={handleSubmit}>
            <h1>Iniciar Sesión</h1>
            <Form.Group>
            <Form.Label>Usuario:</Form.Label>
            <Form.Control className="shadow-none" onChange={(e) => setUsername(e.target.value)} placeholder="Nombre de usuario" type="text" required></Form.Control>
            </Form.Group>
            <Form.Group><br></br>
            <Form.Label className="font-weight-bold">Clave:</Form.Label>
            <Form.Control className="shadow-none" placeholder="***************" onChange={(e) => setPassword(e.target.value)} type="password" required></Form.Control>
            </Form.Group>
            <center><Button type="submit" className="mt-3" variant="primary">Entrar</Button></center><br></br>
            { loginError ? <Alert variant="danger">{errorMessage}</Alert> : "" }
        </Form>
        </div>
        </Container>
        ) : ( <></> ) }
        </>
    );
}

export default Login;