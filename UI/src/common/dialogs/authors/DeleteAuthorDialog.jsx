import { React, useState } from "react";
import { Button, Modal, Row, Alert } from "react-bootstrap";
import axios from "axios";
import { useNavigate } from "react-router-dom";

function DeleteAuthorDialog(props) {
  const authorId = props.authorId;
  const [show, setShow] = useState(props.show);
  const [responseMessage, setResponseMessage] = useState();

  const navigate = useNavigate();

  const handleClose = () => {
    setShow(false);
    props.close();
  };

  const deleteAuthor = async (event) => {
    event.preventDefault();
    console.log("Author ", authorId);

    await axios
      .delete(`/api/authors/${authorId}`)
      .then((response) => {
        console.log(response);
        if (response.data.Status === "Success") {
          console.log("Success");
          setResponseMessage(
            <Alert variant="success">{response.data.Message}</Alert>
          );
          navigate("/authors");
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
        <Modal.Title>Delete author</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Row md={1}>{responseMessage ?? <></>}</Row>
        <Row md={1} style={{ padding: 10 }}>
          Are you sure you want to delete {props.authorName}?
        </Row>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
        <Button variant="danger" onClick={deleteAuthor}>
          Delete
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default DeleteAuthorDialog;
