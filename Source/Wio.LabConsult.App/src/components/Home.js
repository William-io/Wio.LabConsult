import React, { useState } from "react";
import "../components/SearchBox.css";

export const Home = () => {
    const [location, setLocation] = useState("Ceará");

    return (
        <div className="container container-fluid">
            <section id="products" className="container mt-5">
                <div className="home-container">
                    {/* Texto e busca */}
                    <div className="text-search">
                        <h1 className="amazon-ember-text">
                            Cuidamos da Sua Saúde com Qualidade!
                        </h1>
                        <p className="amazon-ember">
                            Agende sua <b>consulta</b> e <b>exame</b> agora mesmo!
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
            </section>
        </div>
    );
};
