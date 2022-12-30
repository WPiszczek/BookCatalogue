import { React, useState } from "react";
import { Button, Modal, Row, Alert } from "react-bootstrap";
import axios from "axios";
import { useNavigate } from "react-router-dom";

function DeleteReviewDialog(props) {
  const reviewId = props.reviewId;
  const [show, setShow] = useState(props.show);
  const [responseMessage, setResponseMessage] = useState();

  const navigate = useNavigate();

  const handleClose = () => {
    setShow(false);
    props.close();
  };

  const deleteReview = async (event) => {
    event.preventDefault();
    console.log("Review ", reviewId);

    await axios
      .delete(`/api/reviews/${reviewId}`)
      .then((response) => {
        console.log(response);
        if (response.data.Status === "Success") {
          console.log("Success");
          setResponseMessage(
            <Alert variant="success">{response.data.Message}</Alert>
          );
          props.hideDeleteReviewDialog();
          props.fetchData();
          // navigate(`/books/${props.bookId}`);
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
        <Modal.Title>Delete review</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Row md={1}>{responseMessage ?? <></>}</Row>
        <Row md={1} style={{ padding: 10 }}>
          Are you sure you want to delete this review?
        </Row>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
        <Button variant="danger" onClick={deleteReview}>
          Delete
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default DeleteReviewDialog;
