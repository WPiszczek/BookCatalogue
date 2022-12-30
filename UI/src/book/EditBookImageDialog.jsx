import { React, useState } from "react";
import {
  Button,
  Modal,
  Row,
  FloatingLabel,
  Form,
  Alert
} from "react-bootstrap";
import axios from "axios";

function EditBookImageDialog(props) {
  const bookId = props.bookId;
  const [show, setShow] = useState(props.show);
  const [image, setImage] = useState();
  const [responseMessage, setResponseMessage] = useState();

  const handleClose = () => {
    setShow(false);
    props.close();
  };

  const editBookImage = async (event) => {
    event.preventDefault();
    console.log("Image ", image);

    const formData = new FormData();
    formData.append("Image", image);
    formData.append("Json", {});

    await axios
      .patch(`/api/books/${bookId}`, formData, {
        "Content-Type": "multipart/form-data"
      })
      .then((response) => {
        console.log(response);
        if (response.data.Status === "Success") {
          console.log("Success");
          setResponseMessage(
            <Alert variant="success">{response.data.Message}</Alert>
          );
          props.fetchSingleBookData();
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
        <Modal.Title>Edit book image - {props.bookTitle}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Row md={1}>{responseMessage ?? <></>}</Row>
        <Row md={1}>
          <FloatingLabel label="Image" className="floating-label">
            <Form.Control
              required
              type="file"
              name="Image"
              onChange={(e) => setImage(e.target.files[0])}
            />
          </FloatingLabel>
        </Row>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
        <Button variant="primary" onClick={editBookImage}>
          Update book image
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default EditBookImageDialog;
