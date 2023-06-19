import React from "react";
import axios from "axios";
import { useState } from "react";
import { API_ROUTES, APP_ROUTES } from "../utils/constants";
import { Link, useNavigate } from "react-router-dom";
import { useUser } from "../lib/customHooks";
import { storeTokenInLocalStorage } from "../lib/common";
import {
  Button,
  Form,
  FormGroup,
  Label,
  Input,
  Container,
  Col,
  FormFeedback,
  Alert,
} from "reactstrap";
import { Spinner } from "reactstrap";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';


toast.configure();

const SignIn = () => {
  const navigate = useNavigate();
  const { user, authenticated } = useUser();
  if (user || authenticated) {
    navigate(APP_ROUTES.CALENDAR);
  }

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [usernameError, setUsernameError] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const signIn = async () => {
    setUsernameError("");
    setPasswordError("");
    setError("");
    if (email.trim() === "") {
      setUsernameError("E-posta adresi gereklidir");
      return;
    }

    if (password.trim() === "") {
      setPasswordError("Parola gereklidir");
      return;
    }

    try {
      setIsLoading(true);
      const response = await axios({
        method: "post",
        url: API_ROUTES.LOGIN,
        data: {
          UserNameOrEmail: email,
          password,
        },
      });
      if (!response?.data?.token) {
        setError("Kullanıcı bulunamadı veya geçersiz kimlik bilgileri");
        console.log("Giriş yapılırken bir sorun oluştu: ", response);
        return;
      }
      storeTokenInLocalStorage(response.data.token);
      window.location.assign(APP_ROUTES.CALENDAR);
      toast.success('Başarıyla oturum açıldı.');
    } catch (err) {
      setError("Giriş yaparken bir hata oluştu");
      console.log("Giriş yaparken bir hata oluştu: ", err);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <Container className="vh-100 d-flex justify-content-center align-items-center">
      <Col sm="12" md="6" lg="4" className="mx-auto">
        <div className="shadow-lg rounded bg-white p-4 d-flex flex-column">
          <h2 className="text-center font-weight-medium mb-4">Giriş Yap</h2>
          <Form className="d-flex flex-column justify-content-around h-75">
            <FormGroup>
              <Label for="email" className="sr-only">
                E-posta
              </Label>
              <Input
                type="email"
                name="email"
                id="email"
                placeholder="E-posta Adresinizi Girin"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                className="border rounded p-2"
                invalid={!!usernameError}
              />
              <FormFeedback>{usernameError}</FormFeedback>
            </FormGroup>
            <FormGroup>
              <Label for="password" className="sr-only">
                Parola
              </Label>
              <Input
                type="password"
                name="password"
                id="password"
                placeholder="*******"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                className="border rounded p-2"
                invalid={!!passwordError}
              />
              <FormFeedback>{passwordError}</FormFeedback>
            </FormGroup>
            {error && <Alert color="danger">{error}</Alert>}
            <Button
              color="dark"
              onClick={signIn}
              className="d-flex justify-content-center align-items-center mt-2"
            >
              {isLoading ? (
                <Spinner size="sm" color="light" className="mr-2" />
              ) : null}
              <span>GİRİŞ YAP</span>
            </Button>
          </Form>
          <div className="text-center small">
            Hesabınız yok mu?
            <Link to="/signup">
              <span className="font-weight-medium text-dark ml-1">
                Kaydolun
              </span>
            </Link>
          </div>
        </div>
      </Col>
    </Container>
  );
};

export default SignIn;
