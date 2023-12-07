using BLL.Interface;
using BLL.Mappers;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IPriceRepository _priceRepository;
    private readonly IUserRepository _userRepository;

    public GameService(IGameRepository gameRepository, IPriceRepository priceRepository, IUserRepository userRepository)
    {
        _gameRepository = gameRepository;
        _priceRepository = priceRepository;
        _userRepository = userRepository;
    }
    
    public GameDTO? CreateGame(GameForm form)
    {
        Game? newGame = _gameRepository.Create(form.ToGame());
        if (newGame is null)
        {
            return null;
        }

        Price newPrice = new Price()
        {
            GameId = newGame.GameId,
            PriceId = 0,
            PriceValue = form.Price,
            UpdateDate = DateTime.Now
        };

        _priceRepository.Create(newPrice);

        return newGame.ToGameDTO();



    }

    public IEnumerable<GameDTO> GetAll()
    {
        return _gameRepository.GetAll().Select(x => x.ToGameDTO());
    }

    public Game? GetById(int gameId)
    {
        Game? game = _gameRepository.Get(gameId);
        return game;
    }

    public BuyingRecapDTO? BuyingGame(int gameId, int userId)
    {
        User? user = _userRepository.Get(userId);
        Game? gameToBuy = GetById(gameId);
        if (user is  null || gameToBuy is null)
        {
            return null;
        } 
        if(_gameRepository.GetById(gameId, userId) is null)
        {
            return null;
        }
        
        GameOfGameList newGame = new GameOfGameList()
        {
            UserId = userId,
            GameId = gameId,
            Date = DateTime.Now,
            PlayinTime = 0,
            GiftId = null,
            Status = 1
        };

        double price = _priceRepository.Get(gameId)!.PriceValue;
        double userWallet = user.Wallet;

        if (userWallet < price)
        {
            return null;
        }
        
        _gameRepository.BuyGame(newGame);
        userWallet -= price;
        user.Wallet = userWallet;

        _userRepository.Update(user);

        BuyingRecapDTO recap = new BuyingRecapDTO()
        {
            GameName = gameToBuy.Name,
            Price = price,
            NewWalletAmount = userWallet,
            BuyingDate = newGame.Date,
            GiftId = null,
            UserId = userId

        };

        return recap;
    }
}