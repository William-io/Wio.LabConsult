import React, { useState, useEffect, Fragment } from "react";
import "../components/SearchBox.css";
import MetaData from "./layout/MetaData";

export const Home = () => {
    const [location, setLocation] = useState("Ceará");
    const [showChat, setShowChat] = useState(false);
    const [showDialog, setShowDialog] = useState(false);
    const [isInteracting, setIsInteracting] = useState(false);
   
    useEffect(() => {
        // Show chat components after 1 second
        const showTimer = setTimeout(() => {
            setShowChat(true);
            setShowDialog(true);
        }, 1000);

        // Hide chat components after 10 seconds if not interacting
        const hideTimer = setTimeout(() => {
            if (!isInteracting) {
                setShowChat(false);
                setShowDialog(false);
            }
        }, 11000);

        return () => {
            clearTimeout(showTimer);
            clearTimeout(hideTimer);
        };
    }, [isInteracting]);

    const handleInteraction = () => {
        setIsInteracting(true);
        setShowChat(true);
        setShowDialog(true);
    };

    return (
        <Fragment>
            <MetaData titulo={'Sua Saúde em boas mãos!'} />
            <section id="consults" className="container mt-5">
                <div className="home-container">
                    {/* Texto e busca */}
                    <div className="text-search">
                        <h1 className="amazon-ember-text">
                            Sua Saúde em Boas Mãos!
                        </h1>
                        <p className="amazon-ember">
                            Agende sua <b>consulta</b> ou <b>exame</b> com rapidez e facilidade.
                        </p>

                        {/* Campo de pesquisa */}
                        <div className="search-container">
                            <div className="search-box">
                                <span className="search-icon"></span>
                                <input
                                    type="text"
                                    placeholder="Procurar por especialidade, médico ou exame."
                                    className="search-input"
                                />
                            </div>

                            {/* Filtro de localização */}
                            <div className="location-box">
                                <span className="location-icon"> </span>
                                <div>
                                    <p className="location-label">Localização</p>
                                    <p className="location-text">{location}</p>
                                </div>
                            </div>
                        </div>

                 
                    </div>

                    {/* Imagem ao lado */}
                    <div className="search-image">
                        <img
                            src="https://www.clinicasim.com/_next/image?url=%2F_next%2Fstatic%2Fmedia%2Fimage12.693941a6.png&w=640&q=75"
                            alt="Médica sorrindo"
                        />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 col-md-6 col-lg-3 my-3">
                        <div class="card">
                            <div class="card-body d-flex align-items-center">
                                <img
                                    class="card-img-top mx-auto"
                                    src="https://www.clinicasim.com/icons/list.svg"
                                    alt="Alergia e Imunologia"
                                    style={{ width: '40px', height: '50px', marginRight: '10px', paddingLeft: '10px' }}
                                />
                                <h5 class="card-title p-3 mb-0">
                                    <a href="">Alergia e Imunologia</a>
                                </h5>
                            </div>
                            <div class="custom-background card-title2" style={{ marginTop: '20px' }}>
                                <div style={{ marginLeft: '10px' }}>
                                    <p>Com a assinatura <b>LaB+</b> partir de R$: 70.90</p>
                                </div>
                                <div>
                                    <a href="#" id="view_btn2" class="btn btn-block">Marcar Consulta</a>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-6 col-lg-3 my-3">
                        <div class="card">
                            <div class="card-body d-flex align-items-center">
                                <img
                                    class="card-img-top mx-auto"
                                    src="https://www.clinicasim.com/icons/list.svg"
                                    alt="Alergia e Imunologia"
                                    style={{ width: '40px', height: '50px', marginRight: '10px', paddingLeft: '10px' }}
                                />
                                <h5 class="card-title p-3 mb-0">
                                    <a href="">Angiologia/Cirurgia Vascular</a>
                                </h5>
                            </div>
                            <div class="custom-background card-title2" style={{ marginTop: '20px' }}>
                                <div style={{ marginLeft: '10px' }}>
                                    <p>Com a assinatura <b>LaB+</b> partir de R$: 230.67</p>
                                </div>
                                <div>
                                    <a href="#" id="view_btn2" class="btn btn-block">Marcar Consulta</a>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-6 col-lg-3 my-3">
                        <div class="card">
                            <div class="card-body d-flex align-items-center" style={{ marginTop: '10px' }}>
                                <img
                                    class="card-img-top mx-auto"
                                    src="https://www.clinicasim.com/icons/exames.svg"
                                    alt="Alergia e Imunologia"
                                    style={{ width: '40px', height: '50px', marginRight: '10px', paddingLeft: '10px' }}
                                />
                                <h5 class="card-title p-3 mb-0">
                                    <a href="">Ultrassom</a>
                                </h5>
                            </div>
                            <div class="custom-background card-title2" style={{ marginTop: '32px' }}>
                                <div style={{ marginLeft: '10px' }}>
                                    <p>Com a assinatura <b>LaB+</b> partir de R$: 128.67</p>
                                </div>
                                <div>
                                    <a href="#" id="view_btn2" class="btn btn-block">Marcar Consulta</a>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-6 col-lg-3 my-3">
                        <div class="card">
                            <div class="card-body d-flex align-items-center" style={{ marginTop: '10px' }}>
                                <img
                                    class="card-img-top mx-auto"
                                    src="https://www.clinicasim.com/icons/exames.svg"
                                    alt="Alergia e Imunologia"
                                    style={{ width: '40px', height: '50px', marginRight: '10px', paddingLeft: '10px' }}
                                />
                                <h5 class="card-title p-3 mb-0">
                                    <a href="">Fonoaudiologia</a>
                                </h5>
                            </div>
                            <div class="custom-background card-title2" style={{ marginTop: '32px' }}>
                                <div style={{ marginLeft: '10px' }}>
                                    <p>Com a assinatura <b>LaB+</b> partir de R$: 128.67</p>
                                </div>
                                <div>
                                    <a href="#" id="view_btn2" class="btn btn-block">Marcar Consulta</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div>
                {showChat && (
                    <button 
                        type="button" 
                        className="chat-button"
                        onClick={handleInteraction}
                        onMouseEnter={handleInteraction}
                    >
                        <img 
                            src="https://via.placeholder.com/40" 
                            className="chat-avatar"
                        />
                    </button>
                )}
                
                {showDialog && (
                    <div 
                        className="chat-dialog"
                        onMouseEnter={handleInteraction}
                    >
                        <p style={{ margin: 0 }}>
                            <strong>Olá! Bem-vindo ao Lab Consult! 👋</strong>
                        </p>
                        <p style={{ margin: '5px 0 0 0', fontSize: '0.9em' }}>
                            Como posso ajudar você hoje?
                        </p>
                    </div>
                )}
            </div>


            </section>

    
        </Fragment>
    );
};
