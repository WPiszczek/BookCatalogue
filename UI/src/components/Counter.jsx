import { React, useState } from "react";
import { Button, Container } from "react-bootstrap";

function Counter() {
  const [currentCount, setCurrentCount] = useState(0);

  function incrementCounter() {
    setCurrentCount(currentCount + 1);
  }

  return (
    <Container>
      <h1>Counter</h1>

      <p>This is a simple example of a React component.</p>

      <p aria-live="polite">
        Current count: <strong>{currentCount}</strong>
      </p>

      <Button variant="primary" onClick={incrementCounter}>
        Increment
      </Button>
    </Container>
  );
}

export default Counter;
