import { React, useState } from "react";
import { Button, Modal, Row, Alert } from "react-bootstrap";
import axios from "axios";
import { useNavigate } from "react-router-dom";

function DeleteBookDialog(props) {
  const bookId = props.bookId;
  const [show, setShow] = useState(props.show);
  const [responseMessage, setResponseMessage] = useState();

  const navigate = useNavigate();

  const handleClose = () => {
    setShow(false);
    props.close();
  };

  const deleteBook = async (event) => {
    event.preventDefault();
    console.log("Book ", bookId);

    await axios
      .delete(`/api/books/${bookId}`)
      .then((response) => {
        console.log(response);
        if (response.data.Status === "Success") {
          console.log("Success");
          setResponseMessage(
            <Alert variant="success">{response.data.Message}</Alert>
          );
          navigate("/books");
        } else {
          console.log("Fail ", response.data.Message);
          setResponseMessage(
            <Alert variant="danger">{response.data.Message}</Alert>
          );
        }
      })
      .catch((error) => {
        console.log(error.message);
        setResponseMessage(
          <Alert variant="danger">Something went wrong. Try again.</Alert>
        );
      });
  };

  return (
    <Modal show={show} onHide={handleClose} size="lg">
      <Modal.Header closeButton>
        <Modal.Title>Delete book</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Row md={1}>{responseMessage ?? <></>}</Row>
        <Row md={1} style={{padding: 10}}>Are you sure you want to delete "{props.bookTitle}"?</Row>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
        <Button variant="primary" onClick={deleteBook}>
          Delete
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default DeleteBookDialog;
