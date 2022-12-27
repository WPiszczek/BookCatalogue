import { React, useState } from "react";
import { Button, Modal } from "react-bootstrap";

function AddBookDialog(props) {
  const [show, setShow] = useState(props.show);

  const handleClose = () => {
    setShow(false);
    props.close();
  };
  const handleShow = () => setShow(true);

  return (
    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Add new book</Modal.Title>
      </Modal.Header>
      <Modal.Body>Add book</Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
        <Button variant="primary" onClick={handleClose}>
          Add book
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default AddBookDialog;
