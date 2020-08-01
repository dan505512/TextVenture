import React from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export const Layout = () => (
  <div>
    <NavMenu />
    <Container>
      {this.props.children}
    </Container>
  </div>
);
