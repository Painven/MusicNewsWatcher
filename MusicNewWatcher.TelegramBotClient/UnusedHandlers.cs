﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicNewsWatcher.TelegramBot;

public static class UnusedHandlers
{
    /*// Send inline keyboard
    // You can process responses in BotOnCallbackQueryReceived handler
    static async Task<Message> SendInlineKeyboard(ITelegramBotClient botClient, Message message)
    {
        await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

        // Simulate longer running task
        await Task.Delay(500);

        InlineKeyboardMarkup inlineKeyboard = new(
            new[]
            {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1.1", "11"),
                        InlineKeyboardButton.WithCallbackData("1.2", "12"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("2.1", "21"),
                        InlineKeyboardButton.WithCallbackData("2.2", "22"),
                    },
            });

        return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: "Choose",
                                                    replyMarkup: inlineKeyboard);
    }

    static async Task<Message> SendReplyKeyboard(ITelegramBotClient botClient, Message message)
    {
        ReplyKeyboardMarkup replyKeyboardMarkup = new(
            new[]
            {
                        new KeyboardButton[] { "1.1", "1.2" },
                        new KeyboardButton[] { "2.1", "2.2" },
            })
        {
            ResizeKeyboard = true
        };

        return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: "Choose",
                                                    replyMarkup: replyKeyboardMarkup);
    }

    static async Task<Message> RemoveKeyboard(ITelegramBotClient botClient, Message message)
    {
        return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: "Removing keyboard",
                                                    replyMarkup: new ReplyKeyboardRemove());
    }

    static async Task<Message> SendFile(ITelegramBotClient botClient, Message message)
    {
        await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);

        const string filePath = @"Files/tux.png";
        using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var fileName = filePath.Split(Path.DirectorySeparatorChar).Last();

        return await botClient.SendPhotoAsync(chatId: message.Chat.Id,
                                              photo: new InputOnlineFile(fileStream, fileName),
                                              caption: "Nice Picture");
    }

    static async Task<Message> RequestContactAndLocation(ITelegramBotClient botClient, Message message)
    {
        ReplyKeyboardMarkup RequestReplyKeyboard = new(
            new[]
            {
                    KeyboardButton.WithRequestLocation("Location"),
                    KeyboardButton.WithRequestContact("Contact"),
            });

        return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: "Who or Where are you?",
                                                    replyMarkup: RequestReplyKeyboard);
    }

    static async Task<Message> Usage(ITelegramBotClient botClient, Message message)
    {
        const string usage = "Usage:\n" +
                             "/inline   - send inline keyboard\n" +
                             "/keyboard - send custom keyboard\n" +
                             "/remove   - remove custom keyboard\n" +
                             "/photo    - send a photo\n" +
                             "/request  - request location or contact";

        return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: usage,
                                                    replyMarkup: new ReplyKeyboardRemove());
    }
}

// Process Inline Keyboard callback data
private static async Task BotOnCallbackQueryReceived(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{
    await botClient.AnswerCallbackQueryAsync(
        callbackQueryId: callbackQuery.Id,
        text: $"Received {callbackQuery.Data}");

    await botClient.SendTextMessageAsync(
        chatId: callbackQuery.Message!.Chat.Id,
        text: $"Received {callbackQuery.Data}");
}

private static async Task BotOnInlineQueryReceived(ITelegramBotClient botClient, InlineQuery inlineQuery)
{
    Console.WriteLine($"Received inline query from: {inlineQuery.From.Id}");

    InlineQueryResult[] results = {
            // displayed result
            new InlineQueryResultArticle(
                id: "1",
                title: "TgBots",
                inputMessageContent: new InputTextMessageContent(
                    "hello"
                )
            )
        };

    await botClient.AnswerInlineQueryAsync(inlineQueryId: inlineQuery.Id,
                                           results: results,
                                           isPersonal: true,
                                           cacheTime: 0);
}

private static Task BotOnChosenInlineResultReceived(ITelegramBotClient botClient, ChosenInlineResult chosenInlineResult)
{
    Console.WriteLine($"Received inline result: {chosenInlineResult.ResultId}");
    return Task.CompletedTask;
}

    */
}
