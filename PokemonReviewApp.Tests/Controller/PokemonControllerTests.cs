﻿using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using PokemonReviewApp.Controllers;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Tests.Controller
{
    public class PokemonControllerTests
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public PokemonControllerTests()
        {
            _pokemonRepository = A.Fake<IPokemonRepository>();
            _reviewRepository = A.Fake<IReviewRepository>();
            _mapper = A.Fake<IMapper>();

        }
        [Fact]
        public void PokemonController_FetPokemons_ReturnOK()
        {
            //Arrange
            var pokemons = A.Fake<ICollection<PokemonDto>>();
            var pokemonList = A.Fake<List<PokemonDto>>();
            A.CallTo(() =>  _mapper.Map<List<PokemonDto>>(pokemons)).Returns(pokemonList);
            var controller = new PokemonController(_pokemonRepository, _reviewRepository, _mapper);
            //Act
            var result = controller.GetPokemons();
            //Assert
            result.Should().NotBeNull();
        }
        [Fact]
        public void PokemonController_CreatePokemon_ReturnOK()
        {
            //Arrange
            int ownerId = 1;
            int catId = 2;
            var pokemonMap = A.Fake<Pokemon>();
            var pokemon = A.Fake<Pokemon>();
            var pokemonCreate = A.Fake<PokemonDto>();
            var pokemons = A.Fake<ICollection<PokemonDto>>();
            var pokmonList = A.Fake<IList<PokemonDto>>();
            A.CallTo(() => _pokemonRepository.GetPokemonTrimToUpper(pokemonCreate)).Returns(pokemon);
            A.CallTo(() => _mapper.Map<Pokemon>(pokemonCreate)).Returns(pokemon);
            A.CallTo(() => _pokemonRepository.CreatePokemon(ownerId, catId, pokemonMap)).Returns(true);
            var controller = new PokemonController(_pokemonRepository, _reviewRepository, _mapper);

            //Act
            var result = controller.CreatePokemon(ownerId, catId, pokemonCreate);

            //Assert
            result.Should().NotBeNull();
        }
    }

}
