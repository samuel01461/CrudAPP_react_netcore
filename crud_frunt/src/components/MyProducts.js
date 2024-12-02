import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { GetMyProducts, DeleteProduct, EditProduct, CreateProduct } from "../redux/actions/productsActions";
import { Form, Button, Card, Col, Modal, Row } from "react-bootstrap";

function MyProducts() {
    const isAuthenticated = useSelector(state => state.auth.isAuthenticated);
    const myProducts = useSelector(state => state.products.myProducts);
    const userId = useSelector(state => state.auth.userId);
    const [showCreateModal, setShowCreateModal] = useState(false);
    const [newProductName, setNewProductName] = useState("");
    const [newProductDescription, setNewProductDescription] = useState("");
    const [newProductPrice, setNewProductPrice] = useState("");

    const [showEditModal, setShowEditModal] = useState(false);
    const [editProductId, setEditProductId] = useState("");
    const [editProductName, setEditProductName] = useState("");
    const [editProductDescription, setEditProductDescription] = useState("");
    const [editProductPrice, setEditProductPrice] = useState("");
    
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const handleSubmitCreate = async (e) => {
        e.preventDefault();
        var new_product = {
            name: newProductName,
            description: newProductDescription,
            price: newProductPrice,
            userId: userId
        }

        dispatch(CreateProduct(new_product));
        setShowCreateModal(false);
    };

    const handleSubmitEdit = async (e) => {
        e.preventDefault();
        var edit_product = {
            id: editProductId,
            name: editProductName,
            description: editProductDescription,
            price: editProductPrice,
            userId: userId
        }

        var new_myProducts = myProducts.map(p => p.id === edit_product.id ? edit_product : p);

        dispatch(EditProduct(edit_product, new_myProducts));

        setShowEditModal(false);
    };

    const handleDelete = async (product) => {
        var new_myProducts = myProducts.filter(p => p.id !== product);
        dispatch(DeleteProduct(product, new_myProducts));
    };

    const onShowCreateModal = async () => {
        setShowCreateModal(true);
    }

    const onCloseCreateModal = async (e) => {
        e.preventDefault();
        setShowCreateModal(false);
    }

    const onShowEditModal = async (p) => {
        setEditProductId(p.id);
        setEditProductName(p.name);
        setEditProductDescription(p.description);
        setEditProductPrice(p.price);

        setShowEditModal(true);
    }

    const onCloseEditModal = async (e) => {
        e.preventDefault();
        setShowEditModal(false);
    }

    useEffect(() => {
        if(!isAuthenticated) {
            navigate("/");
        }
        dispatch(GetMyProducts());
    }, [isAuthenticated, navigate, dispatch]);

    return (
        <>
        <Modal show={showCreateModal}>
            <Modal.Header>
                <Modal.Title>Crear producto</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmitCreate}>
                    <Form.Group>
                        <Form.Label>Nombre: </Form.Label>
                        <Form.Control onChange={(e) => setNewProductName(e.target.value)} required></Form.Control>
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Descripcion: </Form.Label>
                        <Form.Control onChange={(e) => setNewProductDescription(e.target.value)} required></Form.Control>
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Precio: </Form.Label>
                        <Form.Control onChange={(e) => setNewProductPrice(e.target.value)} required></Form.Control>
                    </Form.Group>
                    <div className="mt-2 d-flex justify-content-center">
                    <Button type="submit" className="text-white ms-1" variant="primary">Crear</Button>

                    <Button type="close" onClick={(e) => onCloseCreateModal(e)} variant="secondary">Cerrar</Button>
                    </div>
                </Form>
            </Modal.Body>
        </Modal>
        <div className="d-flex justify-content-end">
            <Button className="mt-3 me-3" variant="success" onClick={() => onShowCreateModal()}>Crear producto</Button>
        </div>

        <Modal show={showEditModal}>
            <Modal.Header>
                <Modal.Title>Editar producto</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmitEdit}>
                    <Form.Group>
                        <Form.Label>Nombre: </Form.Label>
                        <Form.Control value={editProductName} onChange={(e) => setEditProductName(e.target.value)} required></Form.Control>
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Descripcion: </Form.Label>
                        <Form.Control value={editProductDescription} onChange={(e) => setEditProductDescription(e.target.value)} required></Form.Control>
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Precio: </Form.Label>
                        <Form.Control value={editProductPrice} onChange={(e) => setEditProductPrice(e.target.value)} required></Form.Control>
                    </Form.Group>
                    <div className="mt-2 d-flex justify-content-center">
                    <Button type="submit" className="text-white ms-1" variant="primary">Guardar cambios</Button>

                    <Button type="close" onClick={(e) => onCloseEditModal(e)} variant="secondary">Cerrar</Button>
                    </div>
                </Form>
            </Modal.Body>
        </Modal>

        { myProducts.length > 0 ? (
            <Row xs={1} md={3} className="mt-1 ms-1 g-4">
                { myProducts.map(p => (
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
                                <Button onClick={() => onShowEditModal(p)} variant="info text-white">Modificar</Button>
                                <Button onClick={() => handleDelete(p.id)} variant="danger">Eliminar</Button>
                            </Card.Footer>
                        </Card>
                    </Col>
                ))}
            </Row>
        ) : ( <></> )
        }
        </>
    );
}

export default MyProducts;