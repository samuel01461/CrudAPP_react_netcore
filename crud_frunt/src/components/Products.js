import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { Button, Card, Col, Container, Row } from "react-bootstrap";
import { GetAllProducts, DeleteProduct, EditProduct, CreateProduct } from "../redux/actions/productsActions";

function Products() {
    const isAuthenticated = useSelector(state => state.auth.isAuthenticated);
    const products = useSelector(state => state.products.products);
    const userId = useSelector(state => state.auth.userId);
    const isAdmin = useSelector(state => state.auth.isAdmin);

    const navigate = useNavigate();
    const dispatch = useDispatch();

    const handleDelete = async (product) => {
        var new_products = products.filter(p => p.id !== product);
        dispatch(DeleteProduct(product, new_products));
    };

    useEffect(() => {
        if(!isAuthenticated) {
            navigate("/");
        }
        dispatch(GetAllProducts());
    }, [isAuthenticated, navigate, dispatch]);

    return (
        <>
        { products.length > 0 ? (
            <Row xs={1} md={3} className="mt-1 ms-1 g-4">
                { products.map(p => (
                    p.userId === userId ? ( <></> ) : (
                    <Col key={p.id}>
                        <Card className="h-100">
                            <Card.Body>
                                <Card.Title>
                                    { p.name }
                                </Card.Title>
                                <Card.Text>
                                    Descripcion: { p.description }
                                    <br></br>
                                    Precio unitario: <b>{p.price}</b>
                                </Card.Text>
                            </Card.Body>
                            <Card.Footer>
                            <Container className="d-flex justify-content-start">
                                { isAdmin ? (
                                    <Container className="d-flex justify-content-start">
                                        <Button variant="info text-white">Modificar</Button>
                                        <Button onClick={() => handleDelete(p.id)} variant="danger">Eliminar</Button>
                                    </Container>
                                ) : (
                                    <></>
                                )}
                                <Button variant="primary">Comprar</Button>
                                </Container>
                            </Card.Footer>
                        </Card>
                    </Col> )
                ))}
            </Row>
        ) : ( <></> )
        }
        </>
    );
}

export default Products;